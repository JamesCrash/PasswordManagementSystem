using CrashPasswordSystem.Core;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.Search.SearchProducts
{
    public class ListViewModel<T> : ViewModelBase
        where T : class, new()
    {
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

        public int PaginatedCount
        {
            get { return _mirroredItems.Count(); }
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

        public ListViewModel()
        {
            GlobalFilter = new List<string>();

            GoNextPageCommand = new DelegateCommand(() => GoNextPage(), () => CanGoNext);
            GoPreviousPageCommand = new DelegateCommand(() => GoPreviousPage());
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
            CurrentPage++;
            _items.Clear();

            var next = _mirroredItems.Skip(CurrentPage * PageCount).Take(PageCount);

            _items.AddRange(next);

            NotifyPagination();
        }

        protected void GoPreviousPage()
        {
            if (!_mirroredItems.Any())
            {
                return;
            }
            CurrentPage--;
            _items.Clear();

            var next = _mirroredItems.Skip(CurrentPage * PageCount).Take(PageCount);

            _items.AddRange(next);
            NotifyPagination();
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
            _mirroredItems = Items.ToArray();

            if (PageCount > 0)
            {
                CurrentPage = 0;
                SetupPagination(PageCount);
            }
            CollectionViewSource.GetDefaultView(Items)?.Refresh();

            NotifyPagination();
        }

        public void SetupPagination(int pageCount)
        {
            _mirroredItems = Items.ToArray();
            PageCount = pageCount;

            GoNextPage();
        }
    }
}