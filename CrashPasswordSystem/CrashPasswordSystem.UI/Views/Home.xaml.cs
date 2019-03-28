using System.Windows;
using CrashPasswordSystem.UI.ViewModels;

namespace CrashPasswordSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private HomeViewModel _viewModel;
        public Home(HomeViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
