using System;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.ViewModels;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace CrashPasswordSystem.UI.Views
{
    partial class ProductDetails
    {
        public ProductDetails()
        {
            InitializeComponent();

            CloseImage.MouseDown += (s, e) =>
                (DataContext as ProductDetailsViewModel)?.QuitCommand?.Execute(new object());

            CloseBrowseImage.MouseDown += (s, e) =>
                (DataContext as ProductDetailsViewModel)?.CloseBrowseCommand?.Execute(new object());

        }

        public IRegionManager RegionManager { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }

        public override string TargetRegion => Startup.Regions.AsideSection;

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            RegionManager = containerProvider.Resolve<IRegionManager>();

            EventAggregator = containerProvider.Resolve<IEventAggregator>();

            EventAggregator.GetEvent<EditEvent>()
                           .Subscribe(OnEdit, keepSubscriberReferenceAlive: true);

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
            var product = instance as Product;
            if (product == null || product.ProductID <= 0)
            {
                return;
            }

            var context = Container.Resolve<ProductDetailsViewModel>();

            context.ProductOriginal = product;
            DataContext = context;

            if (this.IsActiveOnTargetRegion(RegionManager, TargetRegion))
            {
                this.RemoveFromRegion(TargetRegion, RegionManager);
            }
            RegionManager.AddToRegion(TargetRegion, this);
        }
    }
}
