using System.Text.Json.Serialization;

namespace RentAPlaceAPI.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        
        [JsonIgnore]
        public User? Sender { get; set; }

        [JsonIgnore]
        public User? Receiver { get; set; }
    }
}
