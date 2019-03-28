using System.Collections.Generic;
using System.Threading.Tasks;
using CrashPasswordSystem.Data;



namespace CrashPasswordSystem.UI.Data
{
    public interface IProductDataService
    {
        Task<Product> GetByIdAsync(int productID);
        Task<List<Product>> GetAll();
    }
}