using OrderManager.Base;
using OrderManager.Entities;

namespace OrderManager.Implementation
{
    public interface IOrderRepository : IRepository<Order, long> { }
}
