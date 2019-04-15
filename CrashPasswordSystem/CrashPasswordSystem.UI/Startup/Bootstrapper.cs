using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.Views;
using System.ComponentModel;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.ViewModels;
using Prism.Events;
using CrashPasswordSystem.Services;
using Prism.Mvvm;
using Unity;
using System.Diagnostics;

namespace CrashPasswordSystem.UI.Startup
{
    public class Bootstrapper : IDependencyContainer
    {
        public UnityContainer UnityContainer { get; set; }

        public virtual UnityContainer Bootstrap()
        {
            var builder = new UnityContainer();

            builder.RegisterInstance<IDependencyContainer>(this);
            builder.RegisterSingleton<IEventAggregator, EventAggregator>();
            builder.RegisterType<DetailViewModelBase>();
            builder.RegisterType<DataContext>();

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainWindow>();

            builder.RegisterType<Login>();
            builder.RegisterType<LoginViewModel>();

            builder.RegisterType<Home>();
            builder.RegisterType<HomeViewModel>();

            builder.RegisterType<AddProduct>();

            builder.RegisterType<ProductDetails>();

            builder.RegisterType<ViewModelBase>();

            builder.RegisterType<IUserDataService, UserDataService>();
            builder.RegisterType<IProductDataService, ProductDataService>();
            builder.RegisterType<ICompanyDataService, CompanyDataService>();
            builder.RegisterType<ICategoryDataService, CategoryDataService>();
            builder.RegisterType<ISupplierDataService, SupplierDataService>();

            ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), typeof(MainViewModel));

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            {
                return builder.Resolve(type);
            });

            UnityContainer = builder;

            return UnityContainer;
        }

        public T Resolve<T>()
        {
            Debug.Assert(UnityContainer != null);
            return UnityContainer.Resolve<T>();
        }
    }
}