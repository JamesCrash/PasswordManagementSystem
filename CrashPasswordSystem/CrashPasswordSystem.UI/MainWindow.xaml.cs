using System.Windows;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.ViewModels;

namespace CrashPasswordSystem.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;

            viewModel?.Load();
        }
    }
}
