using CrashPasswordSystem.Core;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    /// <summary>
    /// Represents the ViewModel interaction-class for a 'LoginView' mostly from properties and Event(Locator) and Command actions.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private IUserDataService _UserDataService;

        #region Props
        private readonly BusinessLogic.Validation.Login _login = new BusinessLogic.Validation.Login();

        public override bool IsVisible
        {
            get => User == null | !IsValid;
        }

        public User User { get; set; }
        private UserWrapper _userWrap;

        public UserWrapper userWrap
        {
            get { return _userWrap; }
            set { _userWrap = value; OnPropertyChanged(); }
        }

        public ICommand LogoutCommand { get; set; }

        public ICommand LoginCommand { get; set; }

        private string _IsVisable;
        public string IsVisable
        {
            get { return _IsVisable; }
            set
            {
                base.SetProperty(ref _IsVisable, value);
            }
        }

        private bool _IsValid;
        public bool IsValid
        {
            get { return _IsValid; }
            set
            {
                base.SetProperty(ref _IsValid, value);
                RaisePropertyChanged(nameof(IsVisible));
            }
        }
        
        #endregion

        public LoginViewModel(IDependencyContainer container)
        {
            User = new User();
            userWrap = new UserWrapper(User);
            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
            LogoutCommand = new DelegateCommand(ExecuteLogout);

            _UserDataService = container.Resolve<IUserDataService>();
            EventAggregator = container.Resolve<IEventAggregator>();
        }

        public void Load()
        {
            IsVisable = "Hidden";

#if DEBUG
            userWrap.UserEmail = "nial.mcshane@crashservices.com";
            userWrap.Password = "Password1";

#endif
            userWrap.PropertyChanged += (s, e) =>
            {
                userWrap.GetErrors(e.PropertyName);
                if (e.PropertyName == nameof(userWrap.HasErrors))
                {
                    ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();
        }

        #region Can Execute

            public bool CanExecuteLogin()
        {
            var error = userWrap.HasErrors;
            return !userWrap.HasErrors;
        }
        #endregion

        #region Login Method

        public async void ExecuteLogin()
        {
            var user = await _UserDataService.GetUserByEmail(userWrap.UserEmail);

            if (User != null)
            {
                IsValid = _login.VerifyHash(userWrap.Password, "SHA256", User.UserHash, User.UserSalt);
                if (!IsValid)
                {
                    IsVisable = "Hidden";
                }
                else
                {
                    IsVisable = "Visible";
                    User = user;

                    EventAggregator
                        .GetEvent<LoginEvent>()
                        .Publish(new AuthEventArgs
                        {
                            User = user,
                            Valid = IsValid
                        });
                }
            }
            userWrap.Password = null;
        }

        private void ExecuteLogout()
        {
            IsVisable = "Hidden";
            IsValid = false;
            userWrap.Password = null;

            User = new User();

            EventAggregator
                .GetEvent<LoginEvent>()
                .Publish(new AuthEventArgs
                {
                    User = User,
                    Valid = false
                });

        }
        #endregion
    }
}
