using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrashPasswordSystem.UI.Data
{
    public class UserDataService : IUserDataService
    {
        private Func<DataContext> _contextCreator;

        public UserDataService(Func<DataContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<List<User>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Users.AsNoTracking().ToListAsync();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {

            using (var ctx = _contextCreator())
            {
                return await ctx.Users
                    .SingleAsync(f => f.UserEmail == email);
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
