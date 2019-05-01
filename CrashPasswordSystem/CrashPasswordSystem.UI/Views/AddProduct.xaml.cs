using System;
using System.Windows.Controls;
using CrashPasswordSystem.Core;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.ViewModels;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CrashPasswordSystem.UI.Views
{
    [Module(ModuleName = "AddProduct", OnDemand = true)]
    public partial class AddProduct : UIModule
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        public IContainerProvider Container { get; private set; }
        public IRegionManager RegionManager { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            Container = containerProvider;
            RegionManager = containerProvider.Resolve<IRegionManager>();

            EventAggregator = containerProvider.Resolve<IEventAggregator>();

            EventAggregator.GetEvent<EditEvent>()
                           .Subscribe(OnEdit, keepSubscriberReferenceAlive: true);

            EventAggregator.GetEvent<CloseEvent>()
                           .Subscribe(OnClose, keepSubscriberReferenceAlive: true);

            EventAggregator.GetEvent<SaveEvent<Product>>()
                           .Subscribe(OnSave, keepSubscriberReferenceAlive: true);
        }

        private void OnSave(Product instance)
        {
            Container.Resolve<HomeViewModel>().NotifySave(instance);
        }

        private void OnClose(object obj)
        {
            if (obj == DataContext)
            {
                RegionManager.Regions[Startup.Regions.AsideSection].Remove(this);
            }
        }

        private void OnEdit(object instance)
        {
            DataContext = Container.Resolve<AddProductViewModel>();
            RegionManager.AddToRegion(Startup.Regions.AsideSection, this);
        }
    }
}
