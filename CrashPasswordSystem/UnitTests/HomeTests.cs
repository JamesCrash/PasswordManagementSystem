using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI.ViewModels;
using Moq;
using Prism.Events;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class HomeTests
    {
        public EventAggregator EventAggregator { get; set; }
        public IProductDataService ProductService { get; set; }
        public ISupplierDataService SupplierService { get; set; }
        public ICompanyDataService CompanyService { get; set; }
        public ICategoryDataService CategoryService { get; set; }

        public HomeTests()
        {
            EventAggregator = new EventAggregator();

            ProductService = SetupProducts();

            SupplierService = SetupSuppliers();
            CategoryService = SetupCategories();
            CompanyService = SetupCompanies();
        }

        [Fact]
        public void Home_Load()
        {
            var viewModel = new HomeViewModel(EventAggregator, ProductService, SupplierService, CategoryService, CompanyService);

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
            var viewModel = new HomeViewModel(EventAggregator, ProductService, SupplierService, CategoryService, CompanyService);

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

        #endregion
    }
}
