using System.Collections.Generic;
using System.Threading.Tasks;
using CrashPasswordSystem.Models;

namespace CrashPasswordSystem.UI.Data
{
    public interface ICompanyDataService
    {
        Task<CrashCompany> GetByIdAsync(int companyID);
        Task<List<CrashCompany>> GetAll();
        Task<List<string>> GetAllDesctiption();
    }
}