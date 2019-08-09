using CrashPasswordSystem.UI.Startup;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Windows;
using System.Windows.Threading;

namespace CrashPasswordSystem.UI
{
    /// <summary>
    /// Represents the Main Application container (Prism app).
    /// 
    /// Its primary usage includes the Bootstrapper and Shell (Main) Window.
    /// </summary>
    public partial class App : PrismApplication
    {
        public Bootstrapper Bootstrapper { get; private set; }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. Please inform the admin."
                            + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Bootstrapper = new Bootstrapper(Container);
            
            Bootstrapper.RegisterTypes(containerRegistry);
        }


        /// <summary>
        /// Enables the module catalog initialization so all modules marked as [Module] can be discovered.
        /// 
        /// Module considerations
        /// - Not all the modules in the app are placed in regions. Just a few of them. Then you can have modules 
        /// interacting with other sub-modules or views through EventAggregator.
        /// 
        /// - Modules are intented to be individual and reduce dependencies on them to make them re-usable.
        ///
        /// - Modules can be wired up with MVVM by re-using code. In order to follow MVVM auto-wire model from Prism,
        /// every module may need to define its 'DataContext' to be its ViewModel class.
        /// 
        /// - The ViewModel hierarchy starts from 'MainViewModel' since 'MainWindow' is the root UI. All subsequent views
        /// come into the 'MainViewModel' hierarchy and its ViewModel descentdants (e.g 'Home' or )
        ///  
        /// Follow-up other (basic) modules in the app including 'ApplicationBar' and 'Login' to follow-up in the code
        /// </summary>
        /// <param name="catalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog catalog)
        {
            Bootstrapper.ConfigureModuleCatalog(catalog);
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