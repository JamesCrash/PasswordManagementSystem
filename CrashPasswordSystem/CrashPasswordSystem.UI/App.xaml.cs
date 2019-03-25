using Autofac;
using CrashPasswordSystem.UI.Startup;
using CrashPasswordSystem.UI.ViewModels;
using CrashPasswordSystem.UI.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CrashPasswordSystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var mainWindow = container.Resolve<Login>();
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
