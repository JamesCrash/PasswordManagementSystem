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

        private string _Username;

        public string Username
        {
            get => _Username;
            set => SetProperty(ref _Username, value);
        }
        private string _Password;

        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        public ICommand LoginCommand { get; set; }

        private string _IsVisable;

        public string IsVisable
        {
            get => _IsVisable;
            set => SetProperty(ref _IsVisable, value);
        }

        private bool _IsValid;

        public bool IsValid
        {
            get => _IsValid;
            set => SetProperty(ref _IsValid, value);
        }

        #endregion

        public LoginViewModel()
        {
            IsVisable = "Hidden";
            Username = "nial.mcshane@crashservices.com";
            Password = "Password1";
            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);

            userWrap = new UserWrapper(User);
        }

        #region Can Execute
        private bool CanExecuteLogin()
        {
            return !userWrap.HasErrors;

            //if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            //{
            //    return false;
            //}
            //else
            //{
            //    if (Username != "" && Password != "")
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //}
        }
        #endregion

        #region Login Method

        public async void ExecuteLogin()
        {
            using (var dBContext = new ITDatabaseContext())
            {
                User user = dBContext.Users.FirstOrDefault(s => s.UserEmail == userWrap.UserEmail);

                //_user = new UserWrapper(User);
                //_user.PropertyChanged += (s, e) =>
                //{
                //    if (e.PropertyName == nameof(_user.HasErrors))
                //    {
                //        ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();
                //    }
                //};
                //((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();

                if (user != null)
                {
                    bool isValid = _login.VerifyHash(userWrap.UserHash, "SHA256",
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
