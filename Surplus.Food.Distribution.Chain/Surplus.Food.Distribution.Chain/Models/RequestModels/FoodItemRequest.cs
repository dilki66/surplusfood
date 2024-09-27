namespace Surplus.Food.Distribution.Chain.Models.RequestModels
{
    public class FoodItemRequest
    {
        public string? Image { get; set; }
        public string Category { get; set; }
        public int ServiceTypeId { get; set; }
        public Guid FoodSupplierId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int PriceStatus { get; set; }
        public string? PickupTime { get; set; }
        public string? Location { get; set; }
        public string? Offers { get; set; }
    }
}
