using System.Windows;
using CrashPasswordSystem.UI.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CrashPasswordSystem.UI.Views
{
    [Module(ModuleName = nameof(Login), OnDemand = true)]
    public partial class Login
    {
        LoginViewModel Context => ((LoginViewModel)DataContext);

        public Login()
        {
            InitializeComponent();
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.AddToRegion(Startup.Regions.LoginSection, this);
        }
        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);

            PasswordBox.Password = Context?.userWrap.Password;
            PasswordBox.PasswordChanged += (p, a) => Context.userWrap.Password = PasswordBox.Password;
        }
    }
}