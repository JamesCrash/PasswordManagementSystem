using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI;
using CrashPasswordSystem.UI.Startup;
using CrashPasswordSystem.UI.ViewModels;
using Moq;
using Prism.Events;
using Prism.Mvvm;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace UnitTests
{
    public class HomeTests
    {
        public EventAggregator EventAggregator { get; set; }
        public IProductDataService ProductService { get; set; }
        public ISupplierDataService SupplierService { get; set; }
        public ICompanyDataService CompanyService { get; set; }
        public IDependencyContainer Container { get; }
        public ICategoryDataService CategoryService { get; set; }

        public HomeTests()
        {
            EventAggregator = new EventAggregator();

            ProductService = SetupProducts();

            SupplierService = SetupSuppliers();
            CategoryService = SetupCategories();
            CompanyService = SetupCompanies();

            var containerMock = new Mock<IDependencyContainer>();

            containerMock.Setup(b => b.Resolve<IEventAggregator>())
                         .Returns(EventAggregator);

            containerMock.Setup(b => b.Resolve<IProductDataService>())
                         .Returns(ProductService);

            containerMock.Setup(b => b.Resolve<ISupplierDataService>())
                         .Returns(SupplierService);

            containerMock.Setup(b => b.Resolve<ICategoryDataService>())
                         .Returns(CategoryService);

            containerMock.Setup(b => b.Resolve<ICompanyDataService>())
                         .Returns(CompanyService);

            this.Container = containerMock.Object;
        }

        [Fact]
        public void Home_Load()
        {
            var viewModel = new HomeViewModel(Container);

            Assert.NotNull(viewModel.Categories);
            Assert.True(viewModel.Categories.Any());
            
            Assert.NotNull(viewModel.Products);
            Assert.True(viewModel.Products.Any());

            Assert.NotNull(viewModel.Suppliers);
            Assert.True(viewModel.Suppliers.Any());

            Assert.NotNull(viewModel.Categories);
            Assert.True(viewModel.Categories.Any());
        }

        [Fact]
        public void Home_Filters()
        {
            var viewModel = new HomeViewModel(Container);

            viewModel.FilterData("SelectedCompany", "test");
        }

        #region Private Methods

        private IProductDataService SetupProducts()
        {
            var data = new [] { new Product { ProductDescription = "Test" } };
            var mock = new Mock<IProductDataService>(MockBehavior.Loose);

            mock.Setup(b => b.GetAllAsync())
                        .Returns(Task.FromResult(data.ToList()));

            return mock.Object;
        }

        private ISupplierDataService SetupSuppliers()
        {
            var data = new[] { new Supplier { SupplierName = "Test" } };

            var mock = new Mock<ISupplierDataService>();
            mock.Setup(b => b.GetAllAsync())
                .Returns(Task.FromResult(data.ToList()));

            mock.Setup(b => b.GetAllDesctiption())
                .Returns(Task.FromResult(data.Select(e => e.SupplierName).ToList()));

            return mock.Object;
        }

        private ICategoryDataService SetupCategories()
        {
            var data = new[] { new ProductCategory{ PCName = "Test" } };

            var mock = new Mock<ICategoryDataService>();
            mock.Setup(b => b.GetAllAsync())
                .Returns(Task.FromResult(data.ToList()));

            mock.Setup(b => b.GetAllDesctiption())
                .Returns(Task.FromResult(data.Select(e => e.PCName).ToList()));

            return mock.Object;
        }

        private ICompanyDataService SetupCompanies()
        {
            var data = new[] { new CrashCompany { CCName = "Test" } };

            var mock = new Mock<ICompanyDataService>();
            mock.Setup(b => b.GetAllAsync())
                .Returns(Task.FromResult(data.ToList()));

            mock.Setup(b => b.GetAllDesctiption())
                            .Returns(Task.FromResult(data.Select(e => e.CCName).ToList()));

            return mock.Object;
        }

        public class TestBootstrapper : UnityContainer, IDependencyContainer
        {
            public TestBootstrapper()
            {
                var builder = new UnityContainer();

                builder.RegisterInstance<IDependencyContainer>(this);
                builder.RegisterSingleton<IEventAggregator, EventAggregator>();
                builder.RegisterType<DetailViewModelBase>();
                builder.RegisterType<DataContext>();

                builder.RegisterType<MainViewModel>();
                builder.RegisterType<MainWindow>();
                
                builder.RegisterType<LoginViewModel>();

                builder.RegisterType<HomeViewModel>();

                builder.RegisterType<ViewModelBase>();

                //builder.RegisterType<IUserDataService, UserDataService>();
                //builder.RegisterType<IProductDataService, ProductDataService>();
                //builder.RegisterType<ICompanyDataService, CompanyDataService>();
                //builder.RegisterType<ICategoryDataService, CategoryDataService>();
                //builder.RegisterType<ISupplierDataService, SupplierDataService>();

                //ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), typeof(MainViewModel));

                ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
                {
                    return builder.Resolve(type);
                });
            }

            public T Resolve<T>()
            {
                return Resolve<T>();
            }
        }

        #endregion
    }
}
