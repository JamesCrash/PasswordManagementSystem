﻿using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
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
            set
            {
                base.SetProperty(ref _IsValid, value);
                RaisePropertyChanged(nameof(IsVisible));
            }
        }
        
        #endregion

        public LoginViewModel(IEventAggregator iEventAggregator, IUserDataService userDataService)
        {
            User = new User();
            userWrap = new UserWrapper(User);
            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
            _UserDataService = userDataService;
            EventAggregator = iEventAggregator;
        }

        public void Load()
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
                        .GetEvent<LoggedInEvent>()
                        .Publish(new LoggedInEventArgs
                        {
                            Valid = IsValid
                        });
                }
            }
            userWrap.Password = null;

        }
        #endregion
    }
}
