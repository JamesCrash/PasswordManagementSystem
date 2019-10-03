using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Services;
using Microsoft.EntityFrameworkCore;

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
                return await ctx.Products.FirstOrDefaultAsync(f => f.ProductID == productID);
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Products
                                .Include(b => b.Staff)
                                .ToListAsync();
            }
        }
    }
}
