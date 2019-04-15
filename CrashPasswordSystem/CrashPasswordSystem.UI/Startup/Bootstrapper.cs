﻿using Autofac;
using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.Views;
using System.ComponentModel;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.ViewModels;
using Prism.Events;
using CrashPasswordSystem.Services;

namespace CrashPasswordSystem.UI.Startup
{
    public class Bootstrapper
    {

        public Autofac.IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<DetailViewModelBase>().As<IDetailViewModel>();
            builder.RegisterType<DataContext>().AsSelf();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<Login>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();

            builder.RegisterType<Home>().AsSelf();
            builder.RegisterType<HomeViewModel>().AsSelf();

            builder.RegisterType<AddProduct>().AsSelf();

            builder.RegisterType<ProductDetails>().AsSelf();

            builder.RegisterType<ViewModelBase>().AsSelf();


            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<ProductDataService>().As<IProductDataService>();
            builder.RegisterType<CompanyDataService>().As<ICompanyDataService>();
            builder.RegisterType<CategoryDataService>().As<ICategoryDataService>();
            builder.RegisterType<SupplierDataService>().As<ISupplierDataService>();

            



            return builder.Build();

        }

    }
}
