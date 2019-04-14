using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.Data
{
    public class ProductDataService : IProductDataService
    {
        private readonly Func<DataContext> _contextCreator;

        public ProductDataService(Func<DataContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Product> GetByIdAsync(int productID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Products.AsNoTracking().SingleAsync(f => f.ProductID == productID);
            }
        }

        public async Task<List<Product>> GetAll()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Products.ToListAsync();
            }
        }
    }
}
