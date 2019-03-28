using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrashPasswordSystem.UI.Data;

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
