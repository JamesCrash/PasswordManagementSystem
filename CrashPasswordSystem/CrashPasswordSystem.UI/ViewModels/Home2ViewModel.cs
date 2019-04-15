using CrashPasswordSystem.Services;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class Home2ViewModel
    {
        private IProductDataService _ProductDataService;
        


        public Home2ViewModel(IProductDataService productDataService)
        {
            _ProductDataService = productDataService;

        }
    }
}
