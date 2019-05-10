using Prism.Ioc;
using Prism.Modularity;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CrashPasswordSystem.Core
{
    public class UIModule : UserControl, IModule
    {
        public virtual string TargetRegion { get; } = string.Empty;
        public IContainerProvider Container { get; private set; }

        public UIModule()
        {
            base.Loaded += OnLoaded;
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        public virtual void OnInitialized(IContainerProvider containerProvider)
        {
            Container = containerProvider;
        }

        public virtual void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
