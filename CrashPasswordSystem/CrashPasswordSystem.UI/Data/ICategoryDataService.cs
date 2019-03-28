using System.Collections.Generic;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.Data
{
    public interface ICategoryDataService
    {
        Task<ProductCategory> GetByIdAsync(int productCategoryID);
        Task<List<ProductCategory>> GetAll();
        Task<List<string>> GetAllDesctiption();
    }
}