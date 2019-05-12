using CrashPasswordSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrashPasswordSystem.Services
{
    public interface IDataService<T>
        where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }

    public interface IProductDataService : IDataService<Product> { }

    public interface ICompanyDataService : IDataService<CrashCompany>
    {
        Task<List<string>> GetAllDesctiption();
    }

    public interface ICategoryDataService : IDataService<ProductCategory>
    {
        Task<List<string>> GetAllDesctiption();
    }

    public interface ISupplierDataService : IDataService<Supplier>
    {
        Task<List<string>> GetAllDesctiption();
    }

    public interface IUserDataService : IDataService<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}