using CrashPasswordSystem.UI.Startup;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Windows;
using System.Windows.Threading;

namespace CrashPasswordSystem.UI
{
    public partial class App : PrismApplication
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. Please inform the admin."
                            + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            new Bootstrapper(Container).RegisterTypes(containerRegistry);
        }

        protected IModuleCatalog ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            return catalog;
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            Current.MainWindow.Show();
        }
    }
}