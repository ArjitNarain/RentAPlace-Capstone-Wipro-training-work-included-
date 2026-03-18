namespace FoodDeliveryApp.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        public int SaleId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        public ICollection<OrderItem>? OrderItems { get; set; }
    }

    public class ApplicationUser
    {
        public ApplicationUser()
        {
        }
    }
}
