namespace Surplus.Food.Distribution.Chain.Models.RequestModels
{
    public class ReviewRequest
    {
        public Guid SupplierId { get; set; }
        public string Review { get; set; }
        public decimal? Rating { get; set; }
    }
}
