using CrashPasswordSystem.UI.Startup;
using System;
using System.Windows;
using Unity;

namespace CrashPasswordSystem.UI
{
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. Please inform the admin."
                            + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }

        //public App()
        //{
        //    var vm = new LoginViewModel();
        //    Login login = new Login
        //    {
        //        DataContext = vm
        //    };
        //    vm.OnRequestClose += (s, e) => login.Close();
        //    login.ShowDialog();
        //}
    }
}
