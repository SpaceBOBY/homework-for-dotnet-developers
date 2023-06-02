namespace OrderManager.Models
{
    public class OrderItemModel : OrderItemFrom, IResource<long>
    {
        public long Id { get; set; }

        public new string Code { get; set; }

        public new decimal BasePrice { get; set; }

        public new decimal Amount { get; set; }
    }

    public class OrderItemFrom
    {
        public virtual string Code { get; set; }

        public virtual decimal BasePrice { get; set; }

        public virtual decimal Amount {get;set;}
    }
}
