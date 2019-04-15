using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;

namespace CrashPasswordSystem.UI.Data
{
    public class CategoryDataService : ICategoryDataService
    {
        private readonly Func<DataContext> _contextCreator;

        public CategoryDataService(Func<DataContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<ProductCategory> GetByIdAsync(int productCategoryID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProductCategories.AsNoTracking().SingleAsync(f => f.PCID == productCategoryID);
            }
        }

        public async Task<List<ProductCategory>> GetAllAsync()
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
                return await ctx.ProductCategories.Select(s => s.PCName).ToListAsync();
            }
        }
    }
}
