namespace RentAPlaceAPI.DTOs
{
    public class CreatePropertyDto
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string PropertyType { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
    }
}