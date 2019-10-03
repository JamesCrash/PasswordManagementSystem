using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrashPasswordSystem.UI.Data
{
    public class UserDataService : IUserDataService
    {
        private readonly Func<DataContext> _contextCreator;

        public UserDataService(IDependencyContainer container)
        {
            _contextCreator = () => container.Resolve<DataContext>();
        }

        public async Task<List<User>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Users.ToListAsync();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Users
                    .FirstOrDefaultAsync(f => f.UserID == id);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Users
                    .FirstOrDefaultAsync(f => f.UserEmail == email);
            }
        }

        //public async Task<bool> ValidUserLogin(string usrname, string password)
        //{
        //    return await Context.Meetings.AsNoTracking()
        //        .Include(m => m.Friends)
        //        .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));
        //}

    }
}
