using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrashPasswordSystem.Core
{
    public class UIModule : UserControl, IModule
    {
        public UIModule()
        {
            base.Loaded += OnLoaded;
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        public virtual void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public virtual void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
