using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace RentAPlaceAPI.Models
{
    public class Property
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        //public string Type { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string PropertyType { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public int OwnerId { get; set; }
        
        [JsonIgnore]
        public User? Owner { get; set; }
    }
    
}
