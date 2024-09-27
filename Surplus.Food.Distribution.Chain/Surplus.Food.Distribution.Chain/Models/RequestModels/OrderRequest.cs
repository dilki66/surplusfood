namespace Surplus.Food.Distribution.Chain.Models.RequestModels
{
    public class OrderRequest
    {
        public List<FoodItemIds> FoodItems { get; set; }
        public Guid UserId { get; set; }
        public string? RecieverName { get; set; }
        public int ServiceTypeId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? FoodName { get; set; }
        public int PriceStatusId { get; set; }
        public string? PickupTime { get; set; }
        public string? Location { get; set; }
    }

    public class FoodItemIds
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
