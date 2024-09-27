using System.ComponentModel.DataAnnotations;

namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class FoodSupplierResponse
    {
        public Guid Id { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? SuplierName { get; set; }
        public string? OwnerName { get; set; }
        public bool Status { get; set; }
    }
}
