using Autofac;
using CrashPasswordSystem.UI.Views;
using System.ComponentModel;

namespace CrashPasswordSystem.UI.Startup
{
    class Bootstrapper
    {

        public Autofac.IContainer Bootstrap()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<Login>().AsSelf();
            builder.RegisterType<Home>().AsSelf();
            builder.RegisterType<AddProduct>().AsSelf();
            builder.RegisterType<ProductDetails>().AsSelf();

            return builder.Build();

        }

    }
}
