using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManager.Base;
using OrderManager.Entities;
using OrderManager.Exceptions;
using OrderManager.Implementation;
using OrderManager.Interfaces;
using OrderManager.Models;

namespace OrderManager
{
    public class OrderService : ResourceService<Order, OrderModel, long>, IOrderService
    {
        public OrderService(
            IOrderRepository repository,
            DbContext context,
            IMapper mapper) : base(context, mapper)
        {
            Repository = repository;
        }

        public override IRepository<Order, long> Repository { get; }

        public async Task<OrderModel> PlaceOrderAsync(OrderForm order)
        {
            if (order.DeliveryDate < DateTime.Now)
            {
                throw new OrderPlacementException("Delivery date should be greater than now");
            }

            if (order.OrderItems.Any(x => x.Amount > 999))
            {
                throw new OrderPlacementException("Item amount can't exceed value greater than 999");
            }

            if (order.OrderItems.Any(x => x.Amount % 1 != 0 || x.Amount < 0))
            {
                throw new OrderPlacementException("Item amount should be positive round number");
            }

            var orderTotalPrice = order.OrderItems.Sum(x => x.BasePrice * x.Amount);

            var orderTotalDiscount = 0m;

            switch (order.OrderItems.Sum(x => x.Amount))
            {
                case >= 50:
                    orderTotalDiscount = 0.15m;
                    break;
                case >= 10:
                    orderTotalDiscount = 0.05m;
                    break;
            }

            order.TotalPrice = Math.Round(orderTotalPrice - orderTotalPrice * orderTotalDiscount, 2, MidpointRounding.ToPositiveInfinity);
            order.TotalDiscount = orderTotalDiscount;

            //return await base.CreateAsync(order); 

            return new OrderModel
            {
                CustomerId = order.CustomerId,
                Id = 1,
                DeliveryDate = order.DeliveryDate,
                TotalDiscount = order.TotalDiscount,
                TotalPrice = order.TotalPrice,
                OrderItems = order.OrderItems.Select(x => new OrderItemModel
                {
                    Amount = x.Amount,
                    BasePrice = x.BasePrice,
                    Code = x.Code,
                    Id = 10,
                }).ToArray()
            };
        }

        public async Task<List<OrderModel>> GetCustomerOrdersByCustomerId(long customerId)
        {
            var result = await Repository.Get().Where(x => x.CustomerId == customerId).ToListAsync();

            return Mapper.Map<List<OrderModel>>(result);
        }
    }
}