using CrashPasswordSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrashPasswordSystem.Services
{
    /// <summary>
    /// Represents a data service abstraction for fetching data in the organization. By making it
    /// generic it can be specialized into its own classes (DTOs)
    /// </summary>
    /// <typeparam name="T"></typeparam>
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