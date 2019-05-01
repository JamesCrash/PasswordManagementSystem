using Prism.Events;

namespace CrashPasswordSystem.UI.Event
{
    public class CloseEvent : PubSubEvent<object>
    {
    }

    public class EditEvent : PubSubEvent<object>
    {
    }
    public class SaveEvent : PubSubEvent<object>
    {
    }
    public class SaveEvent<T> : PubSubEvent<T>
        where T : class
    {
    }
}
