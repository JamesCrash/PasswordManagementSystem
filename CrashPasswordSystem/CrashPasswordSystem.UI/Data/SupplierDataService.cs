using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.Data
{
    public class SupplierDataService : ISupplierDataService
    {
        private readonly Func<ITDatabaseContext> _contextCreator;

        public SupplierDataService(Func<ITDatabaseContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Supplier> GetByIdAsync(int supplierID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Suppliers.AsNoTracking().SingleAsync(f => f.SupplierId == supplierID);
            }
        }

        public async Task<List<Supplier>> GetAll()
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
