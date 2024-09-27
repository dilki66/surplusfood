using System.ComponentModel.DataAnnotations;

namespace Surplus.Food.Distribution.Chain.Models.RequestModels
{
    public class FoodSuplierRequest
    {
        [Phone]
        public string? ContactNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? SuplierName { get; set; }
        public string? OwnerName { get; set; }
    }
}
