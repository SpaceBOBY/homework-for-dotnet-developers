using OrderManager.Models;

namespace OrderManager.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> PlaceOrderAsync(OrderForm order);

        Task<List<OrderModel>> GetCustomerOrdersByCustomerId(long customerId);
    }
}
