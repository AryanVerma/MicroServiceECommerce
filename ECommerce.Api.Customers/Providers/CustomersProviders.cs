using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{

    public class CustomersProviders : ICustomersProvider
    {
        private readonly CustomersDbContext _customersDbContext;
        private readonly ILogger<ICustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProviders(CustomersDbContext customersDbContext, ILogger<ICustomersProvider> logger, IMapper mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _customersDbContext = customersDbContext ?? throw new ArgumentNullException(nameof(customersDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            SeedData();

        }

        private void SeedData()
        {
            if (!_customersDbContext.Customers.Any())
            {
                _customersDbContext.Add(new Db.Customer() { Id = 1, Name = "Ram", Address = "Delhi 37" });
                _customersDbContext.Add(new Db.Customer() { Id = 2, Name = "Jam", Address = "Kolkata 38" });
                _customersDbContext.Add(new Db.Customer() { Id = 3, Name = "Shiva", Address = "Gurgaon 33" });
                _customersDbContext.Add(new Db.Customer() { Id = 4, Name = "Dolly", Address = "Bengaluru 32" });
                _customersDbContext.Add(new Db.Customer() { Id = 5, Name = "Karam", Address = "BTM  31" });
                _customersDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                _logger?.LogInformation("querying customers");
                List<Db.Customer> data = await _customersDbContext.Customers.ToListAsync();
                _logger?.LogInformation("queries customers");
                if (data != null && data.Any())
                {
                    _logger?.LogInformation($"{ data.Count }customer(s) found");
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(data);
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
        public async Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerByIdAsync(int Id)
        {
            try
            {
                Db.Customer data = await _customersDbContext.Customers.FirstOrDefaultAsync(r => r.Id == Id);
                if (data != null)
                {
                    var result = _mapper.Map<Db.Customer, Models.Customer>(data);
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
