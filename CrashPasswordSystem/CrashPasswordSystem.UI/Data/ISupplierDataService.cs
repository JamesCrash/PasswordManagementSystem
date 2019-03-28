using System.Collections.Generic;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.Data
{
    public interface ISupplierDataService
    {
        Task<Supplier> GetByIdAsync(int supplierID);
        Task<List<Supplier>> GetAll();
        Task<List<string>> GetAllDesctiption();
    }
}