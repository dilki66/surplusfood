using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class FoodSupplierReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid CustomerId { get; set; }
        public string Review { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
