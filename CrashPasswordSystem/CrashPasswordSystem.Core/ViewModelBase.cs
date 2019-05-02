using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashPasswordSystem.Core
{
    public abstract class ViewModelBase : Prism.Mvvm.BindableBase, INotifyPropertyChanged
    {
        public IEventAggregator EventAggregator { get; protected set; }

        public virtual bool IsVisible { get; }
    }
}
