using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class FoodItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public byte[]? Image { get; set; }
        public string Category { get; set; }
        public int ServiceTypeId { get; set; }
        public Guid FoodSupplierId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int? PriceStatusId { get; set; }
        public bool DeletedFlag { get; set; }
        public string? PickupTime { get; set; }
        public string? Location { get; set; }
        public string? Offers { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual RefServiceType ServiceType { get; set; }

        [ForeignKey("PriceStatusId")]
        public virtual RefPriceStatus PriceStatus { get; set; }

        [ForeignKey("FoodSupplierId")]
        public virtual FoodSupplier FoodSupplier { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; }
    }
}
