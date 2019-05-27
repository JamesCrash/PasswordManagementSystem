using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI;
using CrashPasswordSystem.UI.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace UnitTests
{
    public abstract class CrashPasswordUnitTest
    {
        public IDependencyContainer DependencyContainer { get; protected set; }
        public IUnityContainer IoContainer { get; }

        public CrashPasswordUnitTest()
        {
            IoContainer = new UnityContainer();
        }

        #region Protected Methods

        protected IProductDataService SetupProducts()
        {
            var data = new[] { new Product { ProductDescription = "Test" } };
            var mock = new Mock<IProductDataService>(MockBehavior.Loose);

            mock.Setup(b => b.GetAllAsync())
                        .Returns(Task.FromResult(data.ToList()));

            return mock.Object;
        }

        protected ISupplierDataService SetupSuppliers()
        {
            var data = new[] { new Supplier { SupplierName = "Test" } };

            var mock = new Mock<ISupplierDataService>();
            mock.Setup(b => b.GetAllAsync())
                .Returns(Task.FromResult(data.ToList()));

            mock.Setup(b => b.GetAllDesctiption())
                .Returns(Task.FromResult(data.Select(e => e.SupplierName).ToList()));

            return mock.Object;
        }

        protected ICategoryDataService SetupCategories()
        {
            var data = new[] { new ProductCategory { PCName = "Test" } };

            var mock = new Mock<ICategoryDataService>();
            mock.Setup(b => b.GetAllAsync())
                .Returns(Task.FromResult(data.ToList()));

            mock.Setup(b => b.GetAllDesctiption())
                .Returns(Task.FromResult(data.Select(e => e.PCName).ToList()));

            return mock.Object;
        }

        protected ICompanyDataService SetupCompanies()
        {
            var data = new[] { new CrashCompany { CCName = "Test" } };

            var mock = new Mock<ICompanyDataService>();
            mock.Setup(b => b.GetAllAsync())
                .Returns(Task.FromResult(data.ToList()));

            mock.Setup(b => b.GetAllDesctiption())
                            .Returns(Task.FromResult(data.Select(e => e.CCName).ToList()));

            return mock.Object;
        }

        protected IUserDataService SetupUsers(IDependencyContainer container, DataContext context)
        {
            var users = new[] {
                new User
            {
                UserEmail = "nial.mcshane@crashservices.com",
                UserHash = "G+UsmJ2/FdEBr4urHeNNX35GGMBMncaLjdg1YIbsCr/l2AueUA=="
            }};
            context.Users.Add(users.First());

            return new UserDataService(container);
        }

        protected DataContext ConfigureDbContext(Mock<IDependencyContainer> mock)
        {
            IoContainer.RegisterFactory(typeof(DataContext), 
                f => new DataContext(
                        new DbContextOptionsBuilder<DataContext>()
                        .UseInMemoryDatabase(databaseName: "TestingDb" + Guid.NewGuid().ToString())
                        .Options)
            );

            mock.Setup(b => b.Resolve<DataContext>())
                         .Returns(IoContainer.Resolve<DataContext>());

            return IoContainer.Resolve<DataContext>();
        }

        #endregion
    }
}