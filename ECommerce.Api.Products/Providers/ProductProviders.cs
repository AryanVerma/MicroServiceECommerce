using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductProviders : IProductProvider
    {
        private readonly ProductsDbContext _productsDbContext;
        private readonly ILogger<IProductProvider> _logger;
        private readonly IMapper _mapper;
        public ProductProviders(ProductsDbContext productsDbContext, ILogger<IProductProvider> logger, IMapper mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _productsDbContext = productsDbContext ?? throw new ArgumentNullException(nameof(productsDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            SeedData();

        }

        private void SeedData()
        {
            if (!_productsDbContext.Products.Any())
            {
                _productsDbContext.Add(new Db.Product() { Id = 1, Name = "Keyboard", Inventory = 20, Price = 10 });
                _productsDbContext.Add(new Db.Product() { Id = 2, Name = "Moinitor", Inventory = 10, Price = 10 });
                _productsDbContext.Add(new Db.Product() { Id = 3, Name = "LED", Inventory = 10, Price = 10 });
                _productsDbContext.Add(new Db.Product() { Id = 4, Name = "Mouse", Inventory = 5, Price = 10 });
                _productsDbContext.Add(new Db.Product() { Id = 5, Name = "Charger", Inventory = 30, Price = 10 });
                _productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                List<Db.Product> data = await _productsDbContext.Products.ToListAsync();
                if (data != null && data.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(data);
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
        public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductByIdAsync(int Id)
        {
            try
            {
                Db.Product data = await _productsDbContext.Products.FirstOrDefaultAsync(r => r.Id == Id);
                if (data != null)
                {
                    var result = _mapper.Map<Db.Product, Models.Product>(data);
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
