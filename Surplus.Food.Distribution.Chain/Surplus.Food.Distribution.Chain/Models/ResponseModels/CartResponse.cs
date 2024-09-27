namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class CartResponse
    {
        public List<FoodList> FoodList { get; set; }
        public string? SupplierName { get; set; }
        public decimal Total { get; set; }
    }

    public class FoodList
    {
        public Guid FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public int ServiceTypeId { get; set; }
    }
}
