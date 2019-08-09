using CrashPasswordSystem.UI.Search.SearchProducts;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Windows;

namespace CrashPasswordSystem.UI.Search
{
    /// <summary>
    /// Reprents the UI for Product-Search. It also handles the target region where is going to be displayed
    /// </summary>
    [Module(ModuleName = "SearchProducts", OnDemand = true)]
    public partial class SearchProductsView //: UIModule 
    /*--> Note that the Module inherits from the UIModule for re-using its logic*/
    {
        public SearchProductsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the Context VM for this view. Use this when some specific logic between the UI->VM 
        /// needs to be handled.
        /// </summary>
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

            // Needed to track the visibility for the 'Back/Next' buttons for pagination here.
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
