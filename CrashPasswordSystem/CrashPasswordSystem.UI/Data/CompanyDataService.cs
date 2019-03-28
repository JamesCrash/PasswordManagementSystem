using CrashPasswordSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CrashPasswordSystem.UI.Data
{
    public class CompanyDataService : ICompanyDataService
    {
        private readonly Func<ITDatabaseContext> _contextCreator;

        public CompanyDataService(Func<ITDatabaseContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<CrashCompany> GetByIdAsync(int companyID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.CrashCompanies.AsNoTracking().SingleAsync(f => f.Ccid == companyID);
            }
        }

        public async Task<List<CrashCompany>> GetAll()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.CrashCompanies.ToListAsync();
            }
        }

        public async Task<List<string>> GetAllDesctiption()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.CrashCompanies.Select(s => s.CcName).ToListAsync();
            }
        }
    }
}
