using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.Data
{
    public class ProductDataService : IProductDataService
    {
        private readonly Func<ITDatabaseContext> _contextCreator;

        public ProductDataService(Func<ITDatabaseContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Product> GetByIdAsync(int productID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Products.AsNoTracking().SingleAsync(f => f.ProductId == productID);
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
