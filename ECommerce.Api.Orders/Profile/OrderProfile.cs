using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Profile
{
    
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Orders, Models.Orders>();
            CreateMap<Db.OrderItem, Models.OrderItem>();
        }

    }
}
