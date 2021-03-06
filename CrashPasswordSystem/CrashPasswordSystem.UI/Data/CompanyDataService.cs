﻿using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashPasswordSystem.UI.Data
{
    public class CompanyDataService : ICompanyDataService
    {
        private readonly Func<DataContext> _contextCreator;

        public CompanyDataService(Func<DataContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<CrashCompany> GetByIdAsync(int companyID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.CrashCompanies.FirstOrDefaultAsync(f => f.CCID == companyID);
            }
        }

        public async Task<List<CrashCompany>> GetAllAsync()
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
                return await ctx.CrashCompanies.Select(s => s.CCName).ToListAsync();
            }
        }
    }
}
