using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RentAPlaceAPI.Data;
using RentAPlaceAPI.Models;
using System.Security.Claims;

namespace RentAPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [AllowAnonymous]
        [HttpGet("property/{propertyId}")]
        public IActionResult GetReviews(int propertyId)
        {
            try
            {
                var reviews = _context.Reviews
                    .Where(r => r.PropertyId == propertyId)
                    .Select(r => new
                    {
                        r.Id,
                        r.PropertyId,
                        r.UserId,
                        r.Rating,
                        r.Comment,
                        r.CreatedAt
                    })
                    .ToList();

                return Ok(reviews);
            }
            catch
            {
                return Ok(new List<object>());
            }
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult AddReview([FromBody] Review dto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized();

                var userId = int.Parse(userIdClaim.Value);

                if (dto.Rating < 1 || dto.Rating > 5)
                    return BadRequest("Rating must be between 1 and 5.");

                var review = new Review
                {
                    PropertyId = dto.PropertyId,
                    UserId = userId,
                    Rating = dto.Rating,
                    Comment = dto.Comment,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Reviews.Add(review);
                _context.SaveChanges();

                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not save review: " + ex.Message);
            }
        }
    }
}
