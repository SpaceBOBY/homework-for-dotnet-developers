using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OrderManager.Models
{
    public class OrderModel : OrderForm, IResource<long>
    {
        public long Id { get; set; }

        public new long CustomerId { get; set; }

        public new DateTime DeliveryDate { get; set; }

        public new OrderItemModel[] OrderItems { get; set; }

        public new decimal TotalPrice { get; set; }

        public new decimal TotalDiscount { get; set; }
    }

    public class OrderForm
    {
        [Required]
        public virtual long CustomerId { get; set; }

        [Required]
        public virtual DateTime DeliveryDate { get; set; }

        [Required, MinLength(1), MaxLength(999)]
        public virtual OrderItemFrom[] OrderItems { get; set; }

        public decimal TotalPrice { get; set; } = 0m;

        public decimal TotalDiscount { get; set; } = 0m;
    }
}
