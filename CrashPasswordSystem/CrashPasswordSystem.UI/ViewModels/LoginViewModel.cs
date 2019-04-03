using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CrashPasswordSystem.UI.Event;
using Prism.Events;

namespace CrashPasswordSystem.UI.ViewModels
{

    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {

        private IUserDataService _UserDataService;
        private IEventAggregator _EventAggregator;


        #region Props
        public event EventHandler OnRequestClose;
        private readonly BusinessLogic.Validation.Login _login = new BusinessLogic.Validation.Login();

        public User User { get; set; }
        private UserWrapper _userWrap;

        public UserWrapper userWrap
        {
            get { return _userWrap; }
            set { _userWrap = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }

        private string _IsVisable;

        public string IsVisable
        {
            get { return _IsVisable; }
            set { _IsVisable = value; OnPropertyChanged(); }
        }

        private bool _IsValid;

        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; OnPropertyChanged(); }
        }

        #endregion

        public LoginViewModel(IEventAggregator iEventAggregator, IUserDataService userDataService)
        {
            User = new User();
            userWrap = new UserWrapper(User);
            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
            _UserDataService = userDataService;
            _EventAggregator = iEventAggregator;

        }

        public async Task LoadAsync()
        {
            IsVisable = "Hidden";

            userWrap.UserEmail = "nial.mcshane@crashservices.com";
            userWrap.Password = "Password1";

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
            User user = await _UserDataService.GetUserByEmail(userWrap.UserEmail);

            if (user != null)
            {
                bool isValid = _login.VerifyHash(userWrap.Password, "SHA256",
                      user.UserHash, user.UserSalt);
                if (isValid == true)
                {

                    IsVisable = "Hidden";
                }
                else
                {
                    IsVisable = "Visible";
                }

                _EventAggregator
                    .GetEvent<LoggedInEvent>()
                    .Publish(new LoggedInEventArgs
                    {
                        Valid = isValid
                    });

            }
            userWrap.Password = null;

        }
        #endregion
    }
}
