namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string ContactNo { get; set; }
    }
}
