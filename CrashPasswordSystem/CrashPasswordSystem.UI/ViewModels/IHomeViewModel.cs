using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.ViewModels
{
    public interface IHomeViewModel
    {
        ObservableCollection<Product> Products { get; set; }
        Product SelectedItem { get; set; }
        ICommand ClearFiltersCommand { get; set; }
        ICommand OpenDetailsCommand { get; set; }
        ICommand OpenAddProductCommand { get; set; }
        List<string> Companies { get; set; }
        List<string> Categories { get; set; }
        List<string> Suppliers { get; set; }
        string SelectedCompany { get; set; }
        string SelectedCategory { get; set; }
        string SelectedSupplier { get; set; }
        string SearchBox { get; set; }
        void LoadData();
        void LoadFilters();
        void FilterData(string filter, string value);
        void ClearFilters(object parameter);
        void OpenDetails(object parameter);
        void OpenNewProduct(object parameter);
        event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = null);
    }
}