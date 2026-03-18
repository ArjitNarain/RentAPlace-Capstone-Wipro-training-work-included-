namespace FoodDeliveryApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        public int CartId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<CartItem>? CartItems { get; set; }
    }

}
