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
    /// 
    ///
    /// General considerations for the ViewModels:
    /// 
    /// - Have only (if possible) parameter in the ViewModel to be the 'IDependencyContainer' which is served by Dependency-Injection with Prism
    /// - Don't hold real views references in the code so this VM can be re-used / refactored easily
    /// - Keep communication with other classes (VMs) through EventLocator
    /// 
    /// </summary>
    public abstract class ViewModelBase : Prism.Mvvm.BindableBase, INotifyPropertyChanged
    {
        public IEventAggregator EventAggregator { get; protected set; }

        public virtual bool IsVisible { get; }
    }
}
