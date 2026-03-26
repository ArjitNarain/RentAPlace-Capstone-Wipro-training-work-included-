
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
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult GetMessages()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var messages = _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .Select(m => new MessageResponseDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    Content = m.Content,
                    SentAt = m.SentAt
                })
                .OrderBy(m => m.SentAt)
                .ToList();

            return Ok(messages);
        }

        
        // Send reply message directly using receiverId
        [HttpPost]
        public IActionResult SendMessage([FromBody] CreateMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("Message content cannot be empty.");

            var senderIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (senderIdClaim == null)
                return Unauthorized();

            var senderId = int.Parse(senderIdClaim.Value);

            // Check receiver exists
            var receiver = _context.Users.Find(dto.ReceiverId);
            if (receiver == null)
                return NotFound("Receiver not found.");

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return Ok(new MessageResponseDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                SentAt = message.SentAt
            });
        }

        
        [HttpPost("property/{propertyId}")]
        public IActionResult SendMessageToOwner(int propertyId, [FromBody] CreateMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("Message content cannot be empty.");

            var senderIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (senderIdClaim == null)
                return Unauthorized();

            var senderId = int.Parse(senderIdClaim.Value);

            
            var property = _context.Properties.Find(propertyId);
            if (property == null)
                return NotFound("Property not found.");

            
            if (property.OwnerId == senderId)
                return BadRequest("You cannot message yourself.");

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = property.OwnerId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return Ok(new MessageResponseDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                SentAt = message.SentAt
            });
        }
    }
}

