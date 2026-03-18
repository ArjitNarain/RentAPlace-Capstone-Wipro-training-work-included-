using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RentAPlaceAPI.Data;
using RentAPlaceAPI.Models;
using RentAPlaceAPI.DTOs;
using System.Security.Claims;

namespace RentAPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetProperties()
        {
            var properties = _context.Properties.Select(p => new PropertyResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Location = p.Location,
                Price = p.Price,
                PropertyType = p.PropertyType,
                Description = p.Description,
                ImagePath = p.ImagePath,
                OwnerId = p.OwnerId
            }).ToList();

            return Ok(properties);
        }

        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetProperty(int id)
        {
            var property = _context.Properties.Find(id);

            if (property == null)
                return NotFound("Property not found.");

            return Ok(new PropertyResponseDto
            {
                Id = property.Id,
                Title = property.Title,
                Location = property.Location,
                Price = property.Price,
                PropertyType = property.PropertyType,
                Description = property.Description,
                ImagePath = property.ImagePath,
                OwnerId = property.OwnerId
            });
        }

        
        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? location, [FromQuery] string? type)
        {
            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(p => p.Location.Contains(location));

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(p => p.PropertyType.ToLower() == type.ToLower());

            var result = query.Select(p => new PropertyResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Location = p.Location,
                Price = p.Price,
                PropertyType = p.PropertyType,
                Description = p.Description,
                ImagePath = p.ImagePath,
                OwnerId = p.OwnerId
            }).ToList();

            return Ok(result);
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult AddProperty([FromBody] CreatePropertyDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var property = new Property
            {
                Title = dto.Title,
                Location = dto.Location,
                Price = dto.Price,
                PropertyType = dto.PropertyType,
                Description = dto.Description,
                ImagePath = dto.ImagePath,
                OwnerId = userId
            };

            _context.Properties.Add(property);
            _context.SaveChanges();

            return Ok(new PropertyResponseDto
            {
                Id = property.Id,
                Title = property.Title,
                Location = property.Location,
                Price = property.Price,
                PropertyType = property.PropertyType,
                Description = property.Description,
                ImagePath = property.ImagePath,
                OwnerId = property.OwnerId
            });
        }

        
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateProperty(int id, [FromBody] CreatePropertyDto dto)
        {
            var property = _context.Properties.Find(id);
            if (property == null) return NotFound("Property not found.");

            property.Title = dto.Title;
            property.Location = dto.Location;
            property.Price = dto.Price;
            property.PropertyType = dto.PropertyType;
            property.Description = dto.Description;
            property.ImagePath = dto.ImagePath;

            _context.SaveChanges();

            return Ok(new PropertyResponseDto
            {
                Id = property.Id,
                Title = property.Title,
                Location = property.Location,
                Price = property.Price,
                PropertyType = property.PropertyType,
                Description = property.Description,
                ImagePath = property.ImagePath,
                OwnerId = property.OwnerId
            });
        }

        
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteProperty(int id)
        {
            var property = _context.Properties.Find(id);
            if (property == null) return NotFound("Property not found.");

            _context.Properties.Remove(property);
            _context.SaveChanges();

            return Ok("Property deleted.");
        }
    }
}
