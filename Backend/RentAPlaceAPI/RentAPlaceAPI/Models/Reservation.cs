using System.Text.Json.Serialization;

namespace RentAPlaceAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int UserId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public string Status { get; set; } = "Pending";

        
        [JsonIgnore]
        public Property? Property { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
