using Autofac;
using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.Views;
using System.ComponentModel;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.ViewModels;

namespace CrashPasswordSystem.UI.Startup
{
    class Bootstrapper
    {

        public Autofac.IContainer Bootstrap()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<ITDatabaseContext>().AsSelf();

            builder.RegisterType<Login>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<Home>().AsSelf();
            builder.RegisterType<AddProduct>().AsSelf();
            builder.RegisterType<ProductDetails>().AsSelf();

            builder.RegisterType<UserDataService>().As<IUserDataService>();

            return builder.Build();

        }

    }
}
