using System.Collections.Generic;
using System.Threading.Tasks;
using CrashPasswordSystem.Models;

namespace CrashPasswordSystem.UI.Data
{
    public interface IUserDataService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetUserByEmail(string email);
    }
}