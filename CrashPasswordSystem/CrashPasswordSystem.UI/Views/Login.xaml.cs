using CrashPasswordSystem.UI.ViewModels;
using System.Windows.Controls;

namespace CrashPasswordSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        LoginViewModel Context => ((LoginViewModel)DataContext);
        public Login()
        {
            InitializeComponent();

            Loaded += (s, e) => {
                PasswordBox.Password = Context?.userWrap.Password;
                PasswordBox.PasswordChanged += (p, a) => Context.userWrap.Password = PasswordBox.Password;
            };
        }
    }
}
