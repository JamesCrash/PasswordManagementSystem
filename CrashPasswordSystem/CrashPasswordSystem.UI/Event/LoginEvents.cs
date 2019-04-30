using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace CrashPasswordSystem.UI.Event
{
    public class LoginEvent : PubSubEvent<UserLoginOutEvent>
    {
    }

    public class LogOutEvent : PubSubEvent<UserLoginOutEvent>
    {

    }

    public class UserLoginOutEvent
    {
        public bool Valid { get; set; }  
    }
}
