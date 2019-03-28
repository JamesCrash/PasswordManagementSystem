using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.Views;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using System;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private IUserDataService _UserDataService;

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

        public LoginViewModel(IUserDataService userDataService)
        {

            _UserDataService = userDataService;

            IsVisable = "Hidden";

            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);

            User = new User();
            userWrap = new UserWrapper(User);
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
        private bool CanExecuteLogin()
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


                    //var home = new Home(new HomeViewModel(   
                    //));

                    //home.Show();
                    OnRequestClose(this, new EventArgs());
                    IsVisable = "Hidden";
                }
                else
                {
                    IsVisable = "Visable";
                }
            }
            userWrap.Password = null;

        }
        #endregion
    }
}
