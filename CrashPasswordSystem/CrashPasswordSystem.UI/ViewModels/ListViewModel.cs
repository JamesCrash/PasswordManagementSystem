using CrashPasswordSystem.Core;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.Search.SearchProducts
{
    public class ListViewModel<T> : ViewModelBase
        where T : class, new()
    {
        #region Properties 

        public List<string> GlobalFilter { get; set; }
        public ICommand GoNextPageCommand { get; set; }

        public ICommand GoPreviousPageCommand { get; set; }

        public int PageElementStart
        {
            get
            {
                if (PaginatedCount == 0)
                {
                    return 0;
                }
                return (CurrentPage * PageCount) + 1;
            }
        }

        public int PageElementEnd
        {
            get
            {
                var theEnd = PageElementStart + PageCount - 1;

                if (theEnd > PaginatedCount)
                {
                    return PaginatedCount;
                }
                return theEnd;
            }
        }

        public ObservableCollection<int> RowsPerPageOptions { get; private set; }

        private int _SelectedPageSize;

        public int SelectedPageSize
        {
            get { return _SelectedPageSize; }
            set { base.SetProperty(ref _SelectedPageSize, value); }
        }

        public int PaginatedCount
        {
            get { return _mirroredItems == null ? 0 : _mirroredItems.Count(); }
        }

        private IEnumerable<T> _mirroredItems;

        private ObservableCollection<T> _items;
        public ObservableCollection<T> Items
        {
            get { return _items; }
            set => base.SetProperty(ref _items, value);
        }

        public int PageCount { get; private set; }
        public int CurrentPage { get; private set; } = -1;
        public bool CanGoBack { get => CurrentPage - 1 >= 0; }
        public bool CanGoNext
        {
            get
            {
                if (PageCount <= 0)
                {
                    return false;
                }
                return (CurrentPage + 1) <= TotalPages;
            }
        }

        public int TotalPages
        {
            get
            {
                if (PageCount == 0)
                {
                    return 0;
                }
                return _mirroredItems.Count() / PageCount;
            }
        }

        #endregion

        public ListViewModel()
        {
            GlobalFilter = new List<string>();

            GoNextPageCommand = new DelegateCommand(() => GoNextPage());
            GoPreviousPageCommand = new DelegateCommand(() => GoPreviousPage());

            RowsPerPageOptions = new ObservableCollection<int>(new[] { 5, 10, 30, 50 });
            SelectedPageSize = RowsPerPageOptions.First();
        }

        protected virtual void FilterData(string filter) { }

        protected void GoNextPage()
        {
            if (!_mirroredItems.Any())
            {
                return;
            }
            if (!CanGoNext)
            {
                return;
            }
            GoToPage(++CurrentPage);
        }

        private void GoToPage(int pageNumber)
        {
            _items.Clear();

            var next = _mirroredItems.Skip(pageNumber * PageCount).Take(PageCount);

            _items.AddRange(next);

            CollectionViewSource.GetDefaultView(Items)?.Refresh();

            NotifyPagination();
        }

        protected void GoPreviousPage()
        {
            if (!_mirroredItems.Any())
            {
                return;
            }
            if (!CanGoBack)
            {
                return;
            }
            if (!_mirroredItems.Any())
            {
                return;
            }
            GoToPage(--CurrentPage);
        }

        private void NotifyPagination()
        {
            RaisePropertyChanged(nameof(CanGoBack));
            RaisePropertyChanged(nameof(CanGoNext));
            RaisePropertyChanged(nameof(PageElementStart));
            RaisePropertyChanged(nameof(PageElementEnd));
            RaisePropertyChanged(nameof(PaginatedCount));
        }

        protected void RefreshView()
        {
            CollectionViewSource.GetDefaultView(Items)?.Refresh();

            NotifyPagination();
        }

        public void SetupPagination(int pageCount, T[] items)
        {
            _mirroredItems = items;
            PageCount = pageCount;

            CurrentPage = 0;
            GoToPage(CurrentPage);

            this.PropertyChanged -= OnSelectedPageSizeChanged;
            this.PropertyChanged += OnSelectedPageSizeChanged;
        }

        private void OnSelectedPageSizeChanged(object sender, PropertyChangedEventArgs property)
        {
            if (property.PropertyName == nameof(SelectedPageSize) && SelectedPageSize != PageCount)
            {
                CurrentPage = 0;
                SetupPagination(SelectedPageSize, _mirroredItems.ToArray());
            }
        }
    }
}