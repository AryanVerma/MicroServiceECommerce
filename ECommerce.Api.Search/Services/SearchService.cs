using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;
        private readonly ICustomersService customersService;

        public SearchService(IProductsService productsService,
            IOrdersService ordersService, ICustomersService customersService)
        {
            this.productsService = productsService;
            this.ordersService = ordersService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var customersResult = await customersService.GetCustomerAsync(customerId);
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();

            if (ordersResult.IsSuccess)
            {
                foreach (Models.OrderItem item in from orders in ordersResult.Orders
                                     from item in orders.Items
                                     select item)
                {
                    item.ProductName = productsResult.IsSuccess ?
productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
"Product information is not available";
                }

                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                                customersResult.Customer :
                                new { Name = "Customer information is not available" },
                    Orders = ordersResult.Orders
                };

                return (true, result);
            }
            return (false, null);
        }
    }


}
