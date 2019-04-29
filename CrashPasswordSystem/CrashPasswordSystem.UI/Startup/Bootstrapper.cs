using CrashPasswordSystem.Data;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.ViewModels;
using CrashPasswordSystem.UI.Views;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using System.Configuration;
using System.Diagnostics;
using Unity.Injection;

namespace CrashPasswordSystem.UI.Startup
{
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

            builder.Register<Home>();
            builder.Register<HomeViewModel>();

            builder.Register<AddProduct>();

            builder.Register<ProductDetails>();

            builder.Register<ViewModelBase>();

            builder.Register<IUserDataService, UserDataService>();
            builder.Register<IProductDataService, ProductDataService>();
            builder.Register<ICompanyDataService, CompanyDataService>();
            builder.Register<ICategoryDataService, CategoryDataService>();
            builder.Register<ISupplierDataService, SupplierDataService>();
            
            ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), typeof(MainViewModel));

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            {
                return Container.Resolve(type);
            });
        }

        public T Resolve<T>()
        {
            Debug.Assert(Container != null);
            return Container.Resolve<T>();
        }
    }
}