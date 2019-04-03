using Autofac.Features.Indexed;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Views.Services;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }
        private IMessageDialogService _messageDialogService;
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;

        private IDetailViewModel _selectedDetailViewModel;

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

       

        public ILoginViewModel LoginViewModel { get; }
        public IHomeViewModel HomeViewModel { get; }

        public MainViewModel(IHomeViewModel homeViewModel, ILoginViewModel loginViewModel, IEventAggregator iEventAggregator, IIndex<string, IDetailViewModel> detailViewModelCreator)
        {

            DetailViewModels = new ObservableCollection<IDetailViewModel>();
            _detailViewModelCreator = detailViewModelCreator;
            LoginViewModel = loginViewModel;


            //CreateLogin(loginViewModel.GetType());
            //iEventAggregator
            //    .GetEvent<LoggedInEvent>()
            //    .Subscribe(login);
        }

        public async Task LoadAsync()
        {

            await LoginViewModel.LoadAsync();

        }

        //private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        //{
        //    var detailViewModel = DetailViewModels
        //        .SingleOrDefault(vm => vm.Id == args.Id
        //                               && vm.GetType().Name == args.ViewModelName);

        //    if (detailViewModel == null)
        //    {
        //        detailViewModel = _detailViewModelCreator[args.ViewModelName];
        //        try
        //        {
        //            await detailViewModel.LoadAsync(args.Id);
        //        }
        //        catch
        //        {
        //            await _messageDialogService.ShowInfoDialogAsync("Could not load the entity, " +
        //                                                            "maybe it was deleted in the meantime by another user. " +
        //                                                            "The navigation is refreshed for you.");
        //            //await NavigationViewModel.LoadAsync();
        //            return;
        //        }

        //        DetailViewModels.Add(detailViewModel);
        //    }

        //    SelectedDetailViewModel = detailViewModel;
        //}
        
        //private async void CreateHome(Type viewModelType)
        //{
        //    OnOpenDetailView(
        //        new OpenDetailViewEventArgs
        //        {
        //            ViewModelName = viewModelType.Name
        //        });
        //}
        //private async void CreateLogin(Type viewModelType)
        //{
        //    OnOpenDetailView(
        //        new OpenDetailViewEventArgs
        //        {
        //            ViewModelName = viewModelType.Name
        //        });
        //}

        public async void login(LoggedInEventArgs args)
        {
            if (args.Valid)
            {

            }
        }
    }
}
