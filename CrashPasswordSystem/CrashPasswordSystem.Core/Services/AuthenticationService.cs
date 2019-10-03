using CrashPasswordSystem.Models;

namespace CrashPasswordSystem.Core
{
    public interface IAuthenticationService
    {
        User User { get; set; }
    }

    public class AuthenticationService : IAuthenticationService
    {
        public User User { get; set; }
    }
}