using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using Microsoft.EntityFrameworkCore;

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
                return await ctx.ProductCategories.FirstOrDefaultAsync(f => f.PCID == productCategoryID);
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
