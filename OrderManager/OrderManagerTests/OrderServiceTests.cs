using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OrderManager;
using OrderManager.Entities;
using OrderManager.Exceptions;
using OrderManager.Implementation;
using OrderManager.Interfaces;
using OrderManager.Models;
using System.Linq.Expressions;
using Xunit;

namespace OrderManagerTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<DbContext> _dbContextMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mapperMock.Setup(x => x.Map<OrderModel>(It.IsAny<Order>()))
                .Returns(It.IsAny<OrderModel>());

            //_orderRepositoryMock.Setup(x => x.Get()).Returns(GetFakeOrderList().AsQueryable());
            
            _orderService = new OrderService(_orderRepositoryMock.Object,
                _dbContextMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task Delivery_date_is_not_in_future()
        {
            await Assert.ThrowsAsync<OrderPlacementException>(async () => await _orderService.PlaceOrderAsync(new OrderForm
            {
                DeliveryDate = new DateTime(1999, 1, 1),
            }));
        }

        [Fact]
        public async Task Desired_amount_is_not_positive_round_number()
        {
            await Assert.ThrowsAsync<OrderPlacementException>(async () => await _orderService.PlaceOrderAsync(new OrderForm
            {
                OrderItems = new OrderItemFrom[1]
                {
                    new OrderItemFrom
                    {
                        Amount = 0.5m
                    }
                }
            }));
            await Assert.ThrowsAsync<OrderPlacementException>(async () => await _orderService.PlaceOrderAsync(new OrderForm
            {
                OrderItems = new OrderItemFrom[1]
                {
                    new OrderItemFrom
                    {
                        Amount = -10
                    }
                }
            }));
        }

        [Fact]
        public async Task Desired_amount_is_greater_than_999()
        {
            await Assert.ThrowsAsync<OrderPlacementException>(async () => await _orderService.PlaceOrderAsync(new OrderForm
            {
                OrderItems = new OrderItemFrom[2]
                {
                    new OrderItemFrom
                    {
                        Amount = 10
                    },
                    new OrderItemFrom
                    {
                        Amount = 10000
                    }
                }
            }));
        }

        [Fact]
        public async Task Order_placed()
        {
            var result = await _orderService.PlaceOrderAsync(new OrderForm
            {
                DeliveryDate = new DateTime(DateTime.Now.AddDays(1).Ticks),
                CustomerId = 1,
                OrderItems = new OrderItemFrom[1]
                {
                    new OrderItemFrom
                    {
                        Amount = 10,
                        Code = "BaseKit",
                        BasePrice = 98.99m
                    },
                }
            });

            Assert.Equal(940.41m, result.TotalPrice);
            Assert.Equal(0.05m, result.TotalDiscount);

            result = await _orderService.PlaceOrderAsync(new OrderForm
            {
                DeliveryDate = new DateTime(DateTime.Now.AddDays(1).Ticks),
                CustomerId = 1,
                OrderItems = new OrderItemFrom[1]
                {
                    new OrderItemFrom
                    {
                        Amount = 60,
                        Code = "BaseKit",
                        BasePrice = 98.99m
                    },
                }
            });

            Assert.Equal(5048.49m, result.TotalPrice);
            Assert.Equal(0.15m, result.TotalDiscount);
        }

        [Fact]
        public async Task Get_order_list_by_customer_id()
        {
            var result = await _orderService.GetCustomerOrdersByCustomerId(1);
        }

        public static List<Order> GetFakeOrderList()
        {
            return new List<Order>()
            {
                new Order
                {
                    Id = 1,
                    DeliveryDate = DateTime.Now,
                    CustomerId = 1,
                    OrderItems = new OrderItem[0],
                    TotalDiscount = 0.05m,
                    TotalPrice = 100m
                },
                new Order
                {
                    Id = 2,
                    DeliveryDate = DateTime.Now,
                    CustomerId = 3,
                    OrderItems = new OrderItem[0],
                    TotalDiscount = 0.05m,
                    TotalPrice = 100m
                },
                new Order
                {
                    Id = 3,
                    DeliveryDate = DateTime.Now,
                    CustomerId = 1,
                    OrderItems = new OrderItem[0],
                    TotalDiscount = 0.05m,
                    TotalPrice = 100m
                },
            };
        }
    }
}