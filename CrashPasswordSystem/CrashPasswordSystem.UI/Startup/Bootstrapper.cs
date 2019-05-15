using CrashPasswordSystem.Core;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.ViewModels;
using CrashPasswordSystem.UI.Views;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System.Configuration;
using System.Diagnostics;

namespace CrashPasswordSystem.UI.Startup
{
    public static class Regions
    {
        public const string TopMiddleRegion = "TopMiddleRegion";
        public const string ApplicationBar = "ApplicationBarRegion";
        public const string AsideSection = "AsideSection";
    }

    public class Bootstrapper : IDependencyContainer
    {
        public IContainerProvider Container { get; private set; }

        public Bootstrapper(IContainerProvider container)
        {
            Container = container;
        }
        
        public void RegisterTypes(IContainerRegistry builder)
        {
            builder.RegisterInstance<IDependencyContainer>(this);
            builder.RegisterSingleton<IEventAggregator, EventAggregator>();
            builder.Register<DetailViewModelBase>();

            var sqlServerOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["CrashDbContext"].ConnectionString)
                .Options;

            builder.RegisterInstance(sqlServerOptions);

            builder.Register<MainViewModel>();
            builder.Register<MainWindow>();

            builder.Register<Login>();
            builder.Register<LoginViewModel>();

            builder.Register<HomeViewModel>();

            builder.Register<AddProductViewModel>();

            builder.Register<ProductDetails>();

            builder.Register<ViewModelBase>();

            builder.Register<IUserDataService, UserDataService>();
            builder.Register<IProductDataService, ProductDataService>();
            builder.Register<ICompanyDataService, CompanyDataService>();
            builder.Register<ICategoryDataService, CategoryDataService>();
            builder.Register<ISupplierDataService, SupplierDataService>();
            builder.RegisterSingleton<IAuthenticationService, AuthenticationService>();

            ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), typeof(MainViewModel));

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            {
                return Container.Resolve(type);
            });
        }

        public void ConfigureModuleCatalog(IModuleCatalog catalog)
        {
            catalog.AddModule(typeof(Search.SearchProductsView));
            catalog.AddModule(typeof(ApplicationBar));
            catalog.AddModule(typeof(AddProduct));
            catalog.AddModule(typeof(ProductDetails));
        }

        public T Resolve<T>()
        {
            Debug.Assert(Container != null);
            return Container.Resolve<T>();
        }
    }
}