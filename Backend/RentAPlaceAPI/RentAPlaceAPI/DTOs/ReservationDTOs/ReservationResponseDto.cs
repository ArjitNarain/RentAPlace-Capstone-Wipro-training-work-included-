using System;

namespace RentAPlaceAPI.DTOs
{
    public class ReservationResponseDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? Status { get; set; }
    }
}