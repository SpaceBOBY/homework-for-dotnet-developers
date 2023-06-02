namespace OrderManager.Entities
{
    public sealed class Order : IEntity<long>
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }
    }
}
