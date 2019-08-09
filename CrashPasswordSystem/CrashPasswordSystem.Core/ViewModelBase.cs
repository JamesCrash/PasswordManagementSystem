using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashPasswordSystem.Core
{
    /// <summary>
    /// Represents the base for (almost) any ViewModel class through the application.
    /// 
    /// Intented to re-use common ViewModel tasks like UI Bindings, EventAggregator and Visibility
    /// </summary>
    public abstract class ViewModelBase : Prism.Mvvm.BindableBase, INotifyPropertyChanged
    {
        public IEventAggregator EventAggregator { get; protected set; }

        public virtual bool IsVisible { get; }
    }
}
