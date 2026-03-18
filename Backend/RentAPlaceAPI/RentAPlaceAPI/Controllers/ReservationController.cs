using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RentAPlaceAPI.Data;
using RentAPlaceAPI.Models;
using RentAPlaceAPI.DTOs;
using System.Security.Claims;

namespace RentAPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult GetReservations()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            IQueryable<Reservation> query = _context.Reservations;

            if (role == "Owner")
            {
                // Owner sees reservations for their properties
                var ownerPropertyIds = _context.Properties
                    .Where(p => p.OwnerId == userId)
                    .Select(p => p.Id)
                    .ToList();

                query = query.Where(r => ownerPropertyIds.Contains(r.PropertyId));
            }
            else
            {
                // User sees their own reservations
                query = query.Where(r => r.UserId == userId);
            }

            // Include property title for display
            var reservations = query
                .Include(r => r.Property)
                .Select(r => new
                {
                    r.Id,
                    r.PropertyId,
                    PropertyTitle = r.Property != null ? r.Property.Title : "Unknown",
                    r.UserId,
                    r.CheckIn,
                    r.CheckOut,
                    r.Status
                })
                .ToList();

            return Ok(reservations);
        }

        
        [HttpPost]
        public IActionResult CreateReservation([FromBody] CreateReservationDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var property = _context.Properties.Find(dto.PropertyId);
            if (property == null)
                return NotFound("Property not found.");

            var reservation = new Reservation
            {
                PropertyId = dto.PropertyId,
                UserId = userId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                Status = "Pending"
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return Ok(new
            {
                reservation.Id,
                reservation.PropertyId,
                PropertyTitle = property.Title,
                reservation.UserId,
                reservation.CheckIn,
                reservation.CheckOut,
                reservation.Status
            });
        }

        
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] UpdateReservationStatusDto dto)
        {
            var reservation = _context.Reservations
                .Include(r => r.Property)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound("Reservation not found.");

            reservation.Status = dto.Status;
            _context.SaveChanges();

            return Ok(new
            {
                reservation.Id,
                reservation.PropertyId,
                PropertyTitle = reservation.Property != null ? reservation.Property.Title : "Unknown",
                reservation.UserId,
                reservation.CheckIn,
                reservation.CheckOut,
                reservation.Status
            });
        }
    }
}
