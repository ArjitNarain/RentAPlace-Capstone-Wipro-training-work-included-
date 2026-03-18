namespace FoodDeliveryApp.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class CartItem
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        public int FoodId { get; set; }
        public Food? Food { get; set; }

        public int Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
