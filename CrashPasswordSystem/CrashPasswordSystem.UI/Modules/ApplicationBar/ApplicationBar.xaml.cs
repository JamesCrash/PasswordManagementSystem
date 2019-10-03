using System.Windows;
using CrashPasswordSystem.Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CrashPasswordSystem.UI
{
    [Module(ModuleName = "ApplicationBar", OnDemand = true)]
    public partial class ApplicationBar : UIModule
    {
        public ApplicationBar()
        {
            InitializeComponent();
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.AddToRegion(Startup.Regions.ApplicationBar, this);
        }
    }
}