using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class DeliveryPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string DrivingLicense { get; set; }
        public string? Nic { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
