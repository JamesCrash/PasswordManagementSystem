using CrashPasswordSystem.Core;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI;
using CrashPasswordSystem.UI.Search.SearchProducts;
using CrashPasswordSystem.UI.ViewModels;
using Moq;
using Prism.Events;
using Xunit;

namespace UnitTests
{
    public class MainViewModelTests : CrashPasswordUnitTest
    {
        public EventAggregator EventAggregator { get; }
        public IProductDataService ProductService { get; }
        public ISupplierDataService SupplierService { get; }
        public ICategoryDataService CategoryService { get; }
        public ICompanyDataService CompanyService { get; }
        public IUserDataService UserService { get; }
        public IDependencyContainer Container { get; }

        public MainViewModelTests()
        {
            EventAggregator = new EventAggregator();

            ProductService = SetupProducts();
            SupplierService = SetupSuppliers();
            CategoryService = SetupCategories();
            CompanyService = SetupCompanies();
            
            var containerMock = new Mock<IDependencyContainer>();

            Container = containerMock.Object;

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

            var dataContext = ConfigureDbContext(containerMock);

            UserService = SetupUsers(containerMock.Object, dataContext);

            containerMock.Setup(b => b.Resolve<IUserDataService>())
                         .Returns(UserService);

            containerMock.Setup(b => b.Resolve<IEventAggregator>())
                         .Returns(EventAggregator);

            containerMock.Setup(b => b.Resolve<SearchProductsViewModel>())
                         .Returns(new SearchProductsViewModel(Container));

            containerMock.Setup(b => b.Resolve<SearchProductsViewModel>())
                         .Returns(new SearchProductsViewModel(Container));

            containerMock.Setup(b => b.Resolve<LoginViewModel>())
                                      .Returns(new LoginViewModel(Container));

            containerMock.Setup(b => b.Resolve<IAuthenticationService>())
                                       .Returns(new AuthenticationService());
        }

        [Fact]
        public void LoginTest()
        {
            SetupSuppliers();

            var viewModel = new MainViewModel(Container).LoginViewModel;

            Assert.NotNull(viewModel);
            Assert.True(viewModel.IsVisible);
            Assert.False(viewModel.IsValid);

            Assert.NotNull(viewModel.userWrap);

            viewModel.userWrap.UserEmail = "nial.mcshane@crashservices.com";
            viewModel.userWrap.Password = "Password1";

            viewModel.ExecuteLogin();
        }
    }
}
