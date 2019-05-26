using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI;
using CrashPasswordSystem.UI.Search.SearchProducts;
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

            ConfigureDbContext(containerMock);

            this.Container = containerMock.Object;
        }

        [Fact]
        public void Home_Load()
        {
            var viewModel = new SearchProductsViewModel(Container);

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
        public void Search_Products_DescriptionFilter()
        {
            var viewModel = new SearchProductsViewModel(Container);

            var dataContext = Container.Resolve<DataContext>();

            dataContext.Products.Add(new Product() { ProductDescription = "abc" });
            dataContext.Products.Add(new Product() { ProductDescription = "abc" });
            dataContext.Products.Add(new Product() { ProductDescription = "test" });

            dataContext.SaveChanges();

            viewModel.FilterData("SearchBox", "test");

            Assert.Single(viewModel.Products);
        }


        [Fact]
        public void Prdoucts_Filter_Companies()
        {
            var viewModel = new SearchProductsViewModel(Container);

            var dataContext = Container.Resolve<DataContext>();
            var company1 = new CrashCompany() { CCName = "test company" };
            var company2 = new CrashCompany() { CCName = "test company2" };

            dataContext.Products.Add(new Product() { ProductDescription = "abc", Company = company2 });
            dataContext.Products.Add(new Product() { ProductDescription = "def", Company = company2 });
            dataContext.Products.Add(new Product() { ProductDescription = "product 1", Company = company1 });

            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "ghi" });
            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "jkl" });
            dataContext.CrashCompanies.Add(company1);

            dataContext.SaveChanges();

            viewModel.FilterData("SearchBox", "product 1");
            viewModel.FilterData("SelectedCompany", company1.CCName);

            Assert.Single(viewModel.Products);
        }

        [Fact]
        public void Products_Filter_Company_AndSuppliers()
        {
            var viewModel = new SearchProductsViewModel(Container);

            var dataContext = Container.Resolve<DataContext>();
            var company1 = new CrashCompany() { CCName = "test company" };
            var company2 = new CrashCompany() { CCName = "test company2" };

            dataContext.Products.Add(new Product() { ProductDescription = "abc", Company = company2 });
            dataContext.Products.Add(new Product() { ProductDescription = "def", Company = company2 });
            dataContext.Products.Add(new Product() { ProductDescription = "product 1", Company = company1 });

            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "ghi" });
            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "jkl" });
            dataContext.CrashCompanies.Add(company1);

            dataContext.Suppliers.Add(new Supplier { SupplierName = "ghi" });
            dataContext.Suppliers.Add(new Supplier { SupplierName = "jkl" });
            dataContext.Suppliers.Add(new Supplier { SupplierName = "supplier1" });

            dataContext.SaveChanges();

            viewModel.FilterData(SearchProductsViewModel.FILTER_BY_PRODUCT_NAME, "product 1");
            Assert.Equal(2, viewModel.Products.Count);

            viewModel.FilterData(SearchProductsViewModel.FILTER_BY_COMPANY, company2.CCName);
            Assert.Equal(2, viewModel.Products.Count);

            viewModel.FilterData(SearchProductsViewModel.FILTER_BY_SUPPLIER, company2.CCName);
            Assert.Equal(2, viewModel.Products.Count);
        }
    }
}
