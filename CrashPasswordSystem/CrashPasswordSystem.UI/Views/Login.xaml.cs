using CrashPasswordSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CrashPasswordSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private LoginViewModel _viewModel;

        public Login(LoginViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            //var vm = new LoginViewModel();
            //Login login = new Login
            //{
            //    DataContext = vm
            //};

            _viewModel.OnRequestClose += (s, e) => this.Close();
           // DataContext = vm;
            //login.ShowDialog();



        }


    }
}
