using System;
using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CrashPasswordSystem.UI.Search
{
    [Module(ModuleName="TopMiddleRegion", OnDemand = true)]
    public partial class SearchProductsView : IModule
    {
        public SearchProductsView()
        {
            InitializeComponent();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.AddToRegion("TopMiddleRegion", this);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
