using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Views.Services;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<DetailViewModelBase> DetailViewModels { get; }
        private IMessageDialogService _messageDialogService;
        //private IIndex<string, DetailViewModelBase> _detailViewModelCreator;

        private DetailViewModelBase _selectedDetailViewModel;

        public DetailViewModelBase SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set => base.SetProperty(ref _selectedDetailViewModel, value);
        }

        public LoginViewModel LoginViewModel { get; }

        private HomeViewModel _homeViewModel;
        public HomeViewModel HomeViewModel
        {
            get => _homeViewModel;
            set => base.SetProperty(ref _homeViewModel, value);
        }

        public MainViewModel(IDependencyContainer container)
        {
            DetailViewModels = new ObservableCollection<DetailViewModelBase>();
            //_detailViewModelCreator = detailViewModelCreator;

            LoginViewModel = container.Resolve<LoginViewModel>();
            EventAggregator = container.Resolve<IEventAggregator>();
            HomeViewModel = container.Resolve<HomeViewModel>();

            EventAggregator
                .GetEvent<LoggedInEvent>()
                .Subscribe(Login);
        }

        private void Login(LoggedInEventArgs e)
        {
            HomeViewModel.IsVisible = true;
            LoginViewModel.IsVisible = false;
        }

        public void Load()
        {
            LoginViewModel.Load();
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
