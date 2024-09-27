using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public string? DonerName { get; set; }
        public string? ContactNo { get; set; }
        public string? CardholderName { get; set; }
        public string? PaymentType { get; set; }
        public string? CardNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public string? ExpireDate { get; set; }
        public string? SecurityCode { get; set; }
        public bool Status { get; set; }

        public virtual Order Order { get; set; }
    }
}
