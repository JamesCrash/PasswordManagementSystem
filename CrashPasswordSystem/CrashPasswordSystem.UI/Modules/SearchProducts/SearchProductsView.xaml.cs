using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CrashPasswordSystem.UI.Search
{
    [Module(ModuleName = "SearchProducts", OnDemand = true)]
    public partial class SearchProductsView
    {
        public SearchProductsView()
        {
            InitializeComponent();
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.AddToRegion(Startup.Regions.TopMiddleRegion, this);
        }
    }
}
