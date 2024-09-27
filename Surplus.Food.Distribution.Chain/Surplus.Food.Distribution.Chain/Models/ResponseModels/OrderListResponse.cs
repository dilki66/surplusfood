namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class OrderListResponse
    {
        public List<OrderResponse> Orders { get; set; } = new List<OrderResponse>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
