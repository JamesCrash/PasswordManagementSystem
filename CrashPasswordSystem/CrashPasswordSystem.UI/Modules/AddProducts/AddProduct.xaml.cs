using CrashPasswordSystem.Core;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.ViewModels;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace CrashPasswordSystem.UI.Views
{
    [Module(ModuleName = "AddProduct", OnDemand = true)]
    public partial class AddProduct : UIModule
    {
        public AddProductViewModel Context => DataContext as AddProductViewModel;
        public override string TargetRegion => Startup.Regions.AsideSection;

        public AddProduct()
        {
            InitializeComponent();

            CloseImage.MouseDown += (s, e) => Context?.QuitCommand?.Execute(new object());
        }

        public IRegionManager RegionManager { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            RegionManager = containerProvider.Resolve<IRegionManager>();

            EventAggregator = containerProvider.Resolve<IEventAggregator>();

            EventAggregator.GetEvent<EditEvent>()
                           .Subscribe(OnEdit, keepSubscriberReferenceAlive: true);

            EventAggregator.GetEvent<CloseEvent>()
                           .Subscribe(OnClose, keepSubscriberReferenceAlive: true);

            EventAggregator.GetEvent<CloseEvent>()
                           .Subscribe(OnClose, keepSubscriberReferenceAlive: true);
        }

        private void OnClose(object instance)
        {
            if (instance == DataContext && this.IsActiveOnTargetRegion(RegionManager, TargetRegion))
            {
                this.RemoveFromRegion(TargetRegion, RegionManager);
            }
        }

        private void OnEdit(object instance)
        {
            if (instance == null || !(instance is Type) || ((Type)instance) != typeof(Product))
            {
                return;
            }

            DataContext = Container.Resolve<AddProductViewModel>();

            if (this.IsActiveOnTargetRegion(RegionManager, TargetRegion))
            {
                this.RemoveFromRegion(TargetRegion, RegionManager);
            }
            RegionManager.AddToRegion(TargetRegion, this);
        }
    }
}
