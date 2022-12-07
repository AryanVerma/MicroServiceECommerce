using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Orders> Orders, string ErrorMessage)> GetOrderByCustomerIdAsync(int customerId);
        Task<(bool IsSuccess, IEnumerable<Models.Orders> orders, string ErrorMessage)> GetOrdersAsync();
    }
}
