using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CrashPasswordSystem.UI.Search
{
    [Module(ModuleName = "SearchProducts", OnDemand = true)]
    public partial class SearchProductsView : IModule
    {
        public SearchProductsView()
        {
            InitializeComponent();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.AddToRegion(Startup.Regions.SearchRegion, this);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
