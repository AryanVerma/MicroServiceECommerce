using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{

    public class OrdersProviders : IOrdersProvider
    {
        private readonly OrdersDbContext _ordersDbContext;
        private readonly ILogger<IOrdersProvider> _logger;
        private readonly IMapper _mapper;
        public OrdersProviders(OrdersDbContext ordersDbContext, ILogger<IOrdersProvider> logger, IMapper mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _ordersDbContext = ordersDbContext ?? throw new ArgumentNullException(nameof(_ordersDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            SeedData();

        }

        private void SeedData()
        {
            if (!_ordersDbContext.Orders.Any())
            {
                _ordersDbContext.Orders.Add(new Db.Orders()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _ordersDbContext.Orders.Add(new Db.Orders()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _ordersDbContext.Orders.Add(new Db.Orders()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                _ordersDbContext.SaveChanges();
            }

        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Orders> orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                List<Db.Orders> data = await _ordersDbContext.Orders
                     .Include(o => o.Items).
ToListAsync();
                if (data != null && data.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Orders>, IEnumerable<Models.Orders>>(data);
                    return (true, result, null);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.InnerException.ToString());
                return (false, null, ex.Message);
            }
            return (false, null, "Not Found");
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Orders> Orders, string ErrorMessage)> GetOrderByCustomerIdAsync(int customerId)
        {
            try
            {
                var data = await _ordersDbContext.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Items).ToListAsync();
                if (data != null)
                {
                    var result = _mapper.Map<IEnumerable<Db.Orders>, IEnumerable<Models.Orders>>(data);
                    return (true, result, null);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.InnerException.ToString());
                return (false, null, ex.Message);
            }
            return (false, null, "Not Found");
        }
    }
}
