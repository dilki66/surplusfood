using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public int ServiceTypeId { get; set; }
        public string? PaymentMethod { get; set; }
        public int OrderStatusId { get; set; }
        public string PickupTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PriceStatusId { get; set; }
        public string Location { get; set; }

        public ICollection<Payment> Payments { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual RefServiceType ServiceType { get; set; }

        [ForeignKey("PriceStatusId")]
        public virtual RefPriceStatus PriceStatus { get; set; }

        [ForeignKey("OrderStatusId")]
        public virtual RefOrderStatus OrderStatus { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; }
    }
}
