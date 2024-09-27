namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class FoodItemResponse
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string Category { get; set; }
        public string ServiceTypeId { get; set; }
        public Guid FoodSupplierId { get; set; }
        public decimal Quantity { get; set; }
        public string PriceStatus { get; set; }
        public decimal Price { get; set; }
        public string? Offers { get; set; }
    }
}
