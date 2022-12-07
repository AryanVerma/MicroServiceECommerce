using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interfaces
{
     
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerByIdAsync(int Id);
    }
}
