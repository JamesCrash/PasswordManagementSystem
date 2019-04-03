using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace CrashPasswordSystem.UI.Event
{
    public class LoggedInEvent : PubSubEvent<LoggedInEventArgs>
    {
    }

    public class LoggedInEventArgs
    {
        public bool Valid { get; set; }  
    }
}
