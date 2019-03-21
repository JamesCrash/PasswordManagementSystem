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
        public App()
        {
            var vm = new LoginViewModel();
            Login login = new Login
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => login.Close();
            login.ShowDialog();
        }
    }
}
