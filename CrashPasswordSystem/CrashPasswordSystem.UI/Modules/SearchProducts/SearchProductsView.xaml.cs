using CrashPasswordSystem.UI.Search.SearchProducts;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Windows;

namespace CrashPasswordSystem.UI.Search
{
    [Module(ModuleName = "SearchProducts", OnDemand = true)]
    public partial class SearchProductsView
    {
        public SearchProductsView()
        {
            InitializeComponent();
        }

        public SearchProductsViewModel Context => DataContext as SearchProductsViewModel;
        
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.AddToRegion(Startup.Regions.MainContentSection, this);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);

            if (Context == null)
            {
                return;
            }

            Context.PropertyChanged -= OnGoChanged;
            Context.PropertyChanged += OnGoChanged;
        }

        private void OnGoChanged(object sender, PropertyChangedEventArgs property)
        {
            if (property.PropertyName == nameof(Context.CanGoBack))
            {
                GoPreviousButton.IsEnabled = Context.CanGoBack;
            }
            else if (property.PropertyName == nameof(Context.CanGoNext))
            {
                GoNextButton.IsEnabled = Context.CanGoNext;
            }
        }
    }
}
