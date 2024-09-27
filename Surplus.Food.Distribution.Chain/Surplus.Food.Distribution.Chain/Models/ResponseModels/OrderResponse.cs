namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string FoodCategory { get; set; }
        public string PickupTime { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public string ServiceTypeId { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderStatusId { get; set; }
        public string FoodStatus { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Qty { get; set; }
        public string FoodSupplierName { get; set; }
        public string FoodSupplierAddress { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerContactNo { get; set; }
    }
}
