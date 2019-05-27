using CrashPasswordSystem.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrashPasswordSystem.UI.Search.SearchProducts
{
    public class ListViewModel<T> : ViewModelBase
        where T: class, new()
    {
        public List<string> GlobalFilter { get; set; }


        private ObservableCollection<T> _items;
        public ObservableCollection<T> Items
        {
            get { return _items; }
            set => base.SetProperty(ref _items, value);
        }

        public ListViewModel()
        {
            GlobalFilter = new List<string>();
        }

        protected virtual void FilterData(string filter) { }
    }
}