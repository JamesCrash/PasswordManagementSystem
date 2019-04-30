using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI;
using CrashPasswordSystem.UI.ViewModels;
using Moq;
using Prism.Events;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class HomeTests : CrashPasswordUnitTest
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
    }
}
