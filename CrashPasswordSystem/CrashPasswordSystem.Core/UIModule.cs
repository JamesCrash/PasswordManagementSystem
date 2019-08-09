using Prism.Ioc;
using Prism.Modularity;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CrashPasswordSystem.Core
{
    /// <summary>
    /// Represents a custom UI Module inside the application. Used to reduce the code required to work with Prism
    /// </summary>
    public class UIModule : UserControl, IModule
    {
        /// <summary>
        /// The target region in the UI
        /// </summary>
        public virtual string TargetRegion { get; } = string.Empty;

        /// <summary>
        /// The dependency container
        /// </summary>
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
