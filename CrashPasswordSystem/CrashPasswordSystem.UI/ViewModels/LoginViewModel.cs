using CrashPasswordSystem.BusinessLogic.Validation;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Views;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using System;
using System.Diagnostics;
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

        private UserWrapper _user;
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
        }

        #region Can Execute
        private bool CanExecuteLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                if (Username != "" && Password != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        #endregion

        #region Login Method
        public async void ExecuteLogin()
        {
            using (var dBContext = new ITDatabaseContext())
            {
                User user = dBContext.Users.FirstOrDefault(s => s.UserEmail == Username);

                

                //if (user == null)
                //{
                //    System.Diagnostics.Debug.Write("Soz");
                //    IsVisable = "Visable";
                //}
                //else
                //{

                //    bool isValid = _login.VerifyHash(Password, "SHA256",
                //        user.UserHash, user.UserSalt);

                //    Debug.Assert(false, isValid ? "Valid User" : "Not a Valid User");

                //    if (isValid == true)
                //    {
                //        var home = new Home()
                //        {
                //            DataContext = new HomeViewModel()
                //        };
                //        home.Show();
                //        OnRequestClose(this, new EventArgs());
                //        IsVisable = "Hidden";
                //    }
                //    else
                //    {
                //        IsVisable = "Visable";
                //    }

                //    ;
                    
                //}
            }
        }
        #endregion

    }
}
