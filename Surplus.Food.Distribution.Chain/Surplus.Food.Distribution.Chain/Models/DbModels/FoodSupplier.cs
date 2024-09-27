using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class FoodSupplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SuplierName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string? Nic { get; set; }
        public string? TrainingLicense { get; set; }
        public string OwnerName { get; set; }
        public string? OwnerNic { get; set; }
        public string? Location { get; set; }
        public string BrNo { get; set; }
        public bool Status { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
