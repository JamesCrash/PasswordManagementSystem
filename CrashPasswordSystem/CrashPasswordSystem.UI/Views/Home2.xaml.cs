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
    /// Interaction logic for Home2.xaml
    /// </summary>
    public partial class Home2 : Window
    {
        private Home2ViewModel _viewModel;

        public Home2(Home2ViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            
        }
    }
}
