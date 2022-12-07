using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Profile
{
    /// <summary>
    /// CustomerProfile
    /// </summary>
    /// 11/30/2022 : 11:01 PM
    /// <seealso cref="AutoMapper.Profile" />
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Db.Customer, Models.Customer>();
        }

    }
}
