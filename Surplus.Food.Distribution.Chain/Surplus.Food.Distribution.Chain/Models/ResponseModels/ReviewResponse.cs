namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Review { get; set; }
        public decimal? Rating { get; set; }
    }
}
