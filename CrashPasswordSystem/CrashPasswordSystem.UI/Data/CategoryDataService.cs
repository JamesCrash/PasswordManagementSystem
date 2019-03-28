using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.Data
{
    public class CategoryDataService : ICategoryDataService
    {
        private readonly Func<ITDatabaseContext> _contextCreator;

        public CategoryDataService(Func<ITDatabaseContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<ProductCategory> GetByIdAsync(int productCategoryID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProductCategories.AsNoTracking().SingleAsync(f => f.Pcid == productCategoryID);
            }
        }

        public async Task<List<ProductCategory>> GetAll()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProductCategories.ToListAsync();
            }
        }

        public async Task<List<string>> GetAllDesctiption()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProductCategories.Select(s => s.PcName).ToListAsync();
            }
        }
    }
}
