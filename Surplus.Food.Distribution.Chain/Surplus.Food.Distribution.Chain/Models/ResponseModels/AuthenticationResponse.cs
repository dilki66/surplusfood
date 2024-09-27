namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class AuthenticationResponse
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public Guid UserId { get; set; }
        public Guid? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string CustomerAddress { get; set; }
    }
}
