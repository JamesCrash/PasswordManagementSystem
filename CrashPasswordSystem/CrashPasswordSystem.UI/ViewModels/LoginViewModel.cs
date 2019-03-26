using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.Views;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using System;
using System.Linq;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Props
        //ITDatabaseContext dBContext = new ITDatabaseContext();
        public event EventHandler OnRequestClose;
        private readonly BusinessLogic.Validation.Login _login = new BusinessLogic.Validation.Login();

        public User User { get; set; }
        private UserWrapper _userWrap;

        public UserWrapper userWrap
        {
            get { return _userWrap; }
            set { _userWrap = value; OnPropertyChanged(); }
        }

        //private string _Username;

        //public string Username
        //{
        //    get => _Username;
        //    set => SetProperty(ref _Username, value);
        //}
        //private string _Password;

        //public string Password
        //{
        //    get { return _Password; }
        //    set { _Password = value; OnPropertyChanged(); }
        //}

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

        public LoginViewModel()
        {
            IsVisable = "Hidden";

            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);

            User = new User();
            userWrap = new UserWrapper(User);
            userWrap.UserEmail = "";

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

          //if (string.IsNullOrEmpty(userWrap.UserEmail) || string.IsNullOrEmpty(userWrap.UserHash))
          //  {
          //      return false;
          //  }
          //  else
          //  {
          //      if (userWrap.UserEmail != "" && userWrap.UserHash != "")
          //      {
          //          return true;
          //      }
          //      else
          //      {
          //          return false;
          //      }

          //  }
        }
        #endregion

        #region Login Method

        public async void ExecuteLogin()
        {
            using (var dBContext = new ITDatabaseContext())
            {
                User user = dBContext.Users.FirstOrDefault(s => s.UserEmail == userWrap.UserEmail);

                if (user != null)
                {
                    bool isValid = _login.VerifyHash(userWrap.Password, "SHA256",
                          user.UserHash, user.UserSalt);
                    if (isValid == true)
                    {
                        var home = new Home()
                        {
                            DataContext = new HomeViewModel()
                        };
                        home.Show();
                        OnRequestClose(this, new EventArgs());
                        IsVisable = "Hidden";
                    }
                    else
                    {
                        IsVisable = "Visable";
                    }

                }



            }
        }
        #endregion

    }
}
