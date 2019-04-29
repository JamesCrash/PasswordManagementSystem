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
    public class SupplierDataService : ISupplierDataService
    {
        private readonly Func<DataContext> _contextCreator;

        public SupplierDataService(Func<DataContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Supplier> GetByIdAsync(int supplierID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Suppliers.FirstOrDefaultAsync(f => f.SupplierID == supplierID);
            }
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Suppliers.ToListAsync();
            }
        }

        public async Task<List<string>> GetAllDesctiption()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Suppliers.Select(s => s.SupplierName).ToListAsync();
            }
        }
    }
}
