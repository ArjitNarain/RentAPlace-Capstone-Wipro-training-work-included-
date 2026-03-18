namespace RentAPlaceAPI.DTOs
{
    public class UpdateReservationStatusDto
    {
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled
    }
}
