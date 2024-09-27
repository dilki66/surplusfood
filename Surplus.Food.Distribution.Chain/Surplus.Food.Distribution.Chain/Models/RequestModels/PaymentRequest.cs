namespace Surplus.Food.Distribution.Chain.Models.RequestModels
{
    public class PaymentRequest
    {
        public string? DonerName { get; set; }
        public string? ContactNo { get; set; }
        public string? CardholderName { get; set; }
        public string? PaymentType { get; set; }
        public string? CardNo { get; set; }
        public decimal Amount { get; set; }
        public string? ExpireDate { get; set; }
        public string? SecurityCode { get; set; }
    }
}
