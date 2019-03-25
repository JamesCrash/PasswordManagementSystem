using CrashPasswordSystem.BusinessLogic.Encryption;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Views;
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

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }

        private string _IsVisable;

        public string IsVisable
        {
            get { return _IsVisable; }
            set { _IsVisable = value; OnPropertyChanged(); }
        }

        private bool _IsValid;
        private readonly Encryption _encryption = new Encryption();

        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; OnPropertyChanged(); }
        }

        #endregion

        public LoginViewModel()
        {
            IsVisable = "Hidden";
            Username = "james.mitchell@crashservices.com";
            Password = "Password";
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        #region Can Execute
        private bool CanExecuteLogin(object parameter)
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
        public async void ExecuteLogin(object parameter)
        {
            using (var dBContext = new ITDatabaseContext())
            {
                User user = dBContext.Users.FirstOrDefault(s => s.UserEmail == Username);

                if (user == null)
                {
                    System.Diagnostics.Debug.Write("Soz");
                    IsVisable = "Visable";
                }
                else
                {



                    bool isValid = _encryption.VerifyHash(Password, "SHA256",
                        user.UserHash, user.UserSalt);

                    Debug.Assert(false, isValid ? "Valid User" : "Not a Valid User");

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

                    ;
                    
                }
            }
        }
        #endregion

    }
}
