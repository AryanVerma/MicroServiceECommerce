using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        
        private readonly IOrdersProvider  orderProvider;
        public OrdersController(IOrdersProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }

        [HttpGet("{customerid}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int customerid)
        {
            (bool IsSuccess, IEnumerable< Models.Orders> orders, string ErrorMessage) data = await this.orderProvider.GetOrderByCustomerIdAsync(customerid);
            if (data.IsSuccess)
            {
                return Ok(data.orders);
            }
            return NotFound();
        }
    }
}
