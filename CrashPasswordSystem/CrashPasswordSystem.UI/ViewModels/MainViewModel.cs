using CrashPasswordSystem.Core;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Search.SearchProducts;
using CrashPasswordSystem.UI.Views.Services;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    /// <summary>
    /// Represents the root ViewModel for the application. It consists in a combination of services, included:
    /// 
    /// - Authentication for the application
    /// - Exit
    /// - Current View (Home-> Module)
    /// - Login View (Login-> Module)
    /// 
    /// Modules above are injected into the UI through Prism so they get wired-up by the 'prism:ViewModelLocator.AutoWireViewModel="True"'
    /// feature of Prism. Then the ViewModels can be obtained in every module individually.
    /// 
    /// Follow-up other ViewModels in the root including:
    /// 
    /// - 'HomeViewModel'
    /// - 'LoginViewModel'
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<DetailViewModelBase> DetailViewModels { get; }
        public IAuthenticationService AuthenticationService { get; set; }

        private IMessageDialogService _messageDialogService;
        //private IIndex<string, DetailViewModelBase> _detailViewModelCreator;

        public ICommand ExitCommand { get; set; }

        public override bool IsVisible
        {
            get => LoginViewModel != null
                    && LoginViewModel.User != null && LoginViewModel.IsValid;
        }

        public LoginViewModel LoginViewModel { get; }

        private ViewModelBase _homeViewModel;
        public ViewModelBase HomeViewModel
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
            HomeViewModel = container.Resolve<SearchProductsViewModel>();
            AuthenticationService = container.Resolve<IAuthenticationService>();

            EventAggregator
                .GetEvent<LoginEvent>()
                .Subscribe(LogInOut);

            EventAggregator
                .GetEvent<LogoutEvent>()
                .Subscribe(LogInOut);

            ExitCommand = new DelegateCommand(() => App.Current?.Shutdown());
        }

        private void LogInOut(AuthEventArgs e)
        {
            AuthenticationService.User = e.User;
            RaisePropertyChanged(nameof(IsVisible));
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
    }
}
