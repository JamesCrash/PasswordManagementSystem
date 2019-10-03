using CrashPasswordSystem.Models;
using Prism.Events;

namespace CrashPasswordSystem.UI.Event
{
    public class LoginEvent : PubSubEvent<AuthEventArgs>
    {
    }

    public class LogoutEvent : PubSubEvent<AuthEventArgs>
    {

    }

    public class AuthEventArgs
    {
        public User User { get; set; }
        public bool Valid { get; set; }  
    }
}
