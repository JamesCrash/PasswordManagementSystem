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
        public ICategoryDataService CategoryService { get; set; }

        public HomeTests()
        {
            EventAggregator = new EventAggregator();

            ProductService = SetupProducts();
            SupplierService = SetupSuppliers();
            CategoryService = SetupCategories();
            CompanyService = SetupCompanies();

            var mock = new Mock<IDependencyContainer>();

            mock.Setup(b => b.Resolve<IEventAggregator>())
                          .Returns(EventAggregator);

            mock.Setup(b => b.Resolve<IProductDataService>())
                          .Returns(ProductService);

            mock.Setup(b => b.Resolve<ISupplierDataService>())
                          .Returns(SupplierService);

            mock.Setup(b => b.Resolve<ICategoryDataService>())
                          .Returns(CategoryService);

            mock.Setup(b => b.Resolve<ICompanyDataService>())
                          .Returns(CompanyService);

            ConfigureDbContext(mock);

            this.DependencyContainer = mock.Object;
        }

        [Fact]
        public void Home_Load()
        {
            var dataContext = DependencyContainer.Resolve<DataContext>();

            dataContext.Products.Add(new Product() { ProductDescription = "abc" });
            dataContext.Products.Add(new Product() { ProductDescription = "abc" });
            dataContext.Products.Add(new Product() { ProductDescription = "test" });

            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "ghi" });
            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "jkl" });

            dataContext.Suppliers.Add(new Supplier { SupplierName = "supplier1" });
            dataContext.Suppliers.Add(new Supplier { SupplierName = "ghi" });

            dataContext.ProductCategories.Add(new ProductCategory { PCName = "supplier1" });
            dataContext.ProductCategories.Add(new ProductCategory { PCName = "ghi" });

            dataContext.SaveChanges();

            var viewModel = new SearchProductsViewModel(DependencyContainer);

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
            var viewModel = new SearchProductsViewModel(DependencyContainer);

            var dataContext = DependencyContainer.Resolve<DataContext>();

            dataContext.Products.Add(new Product() { ProductDescription = "abc" });
            dataContext.Products.Add(new Product() { ProductDescription = "abc" });
            dataContext.Products.Add(new Product() { ProductDescription = "test" });

            dataContext.SaveChanges();

            viewModel.SearchBox = "test";

            Assert.Single(viewModel.Products);
        }

        [Fact]
        public void Products_Filter_Companies()
        {
            var viewModel = new SearchProductsViewModel(DependencyContainer);

            var dataContext = DependencyContainer.Resolve<DataContext>();
            var company1 = new CrashCompany() { CCName = "test company" };
            var company2 = new CrashCompany() { CCName = "test company2" };

            dataContext.Products.Add(new Product() { ProductDescription = "abc", Company = company2 });
            dataContext.Products.Add(new Product() { ProductDescription = "def", Company = company2 });
            dataContext.Products.Add(new Product() { ProductDescription = "product 1", Company = company1 });

            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "ghi" });
            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "jkl" });
            dataContext.CrashCompanies.Add(company1);

            dataContext.SaveChanges();

            viewModel.SearchBox = "product 1";
            Assert.Single(viewModel.Products);

            viewModel.SelectedCompany = company1.CCName;
            Assert.Single(viewModel.Products);

            var expected = viewModel.Products.First();

            Assert.NotNull(expected.Company);
            Assert.NotNull(expected.Company.Products);
            Assert.Equal(1, expected.Company.Products.Count);
        }

        [Fact]
        public void Products_Filter_Company_AndSuppliers()
        {
            var viewModel = new SearchProductsViewModel(DependencyContainer);

            var dataContext = DependencyContainer.Resolve<DataContext>();

            var supplier1 = new Supplier { SupplierName = "supplier1" };
            var supplier2 = new Supplier { SupplierName = "ghi" };

            dataContext.Suppliers.Add(supplier2);
            dataContext.Suppliers.Add(new Supplier { SupplierName = "jkl" });
            dataContext.Suppliers.Add(supplier1);

            var company1 = new CrashCompany() { CCName = "test company" };
            var company2 = new CrashCompany() { CCName = "test company2" };
            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "ghi" });
            dataContext.CrashCompanies.Add(new CrashCompany() { CCName = "jkl" });
            dataContext.CrashCompanies.Add(company1);

            dataContext.Products.Add(new Product() { ProductDescription = "product-2", Company = company2, Supplier = supplier1 });
            dataContext.Products.Add(new Product() { ProductDescription = "product-1", Company = company2, Supplier = supplier2 });
            dataContext.Products.Add(new Product() { ProductDescription = "def-ghijk", Company = company1, Supplier = supplier1 });

            dataContext.SaveChanges();

            viewModel.SearchBox = "product-";
            Assert.Equal(2, viewModel.Products.Count);

            viewModel.SelectedCompany = company2.CCName;
            Assert.Equal(2, viewModel.Products.Count);

            viewModel.SelectedSupplier = supplier1.SupplierName;
            Assert.Single(viewModel.Products);

            var expected = viewModel.Products.First();

            Assert.NotNull(expected.Company);
            Assert.NotNull(expected.Company.Products);

            Assert.Equal(2, expected.Company.Products.Count);
        }
    }
}
