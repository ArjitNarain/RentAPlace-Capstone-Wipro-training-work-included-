namespace FoodDeliveryApp.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int SaleId { get; set; }
        public Sale? Sale { get; set; }

        public int FoodId { get; set; }
        public Food? Food { get; set; }

        public int Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount { get; set; }
    }

}
