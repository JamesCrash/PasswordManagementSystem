using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Wrapper;

namespace CrashPasswordSystem.UI.ViewModels
{
    public interface ILoginViewModel
    {
        event EventHandler OnRequestClose;
        User User { get; set; }
        UserWrapper userWrap { get; set; }
        ICommand LoginCommand { get; set; }
        string IsVisable { get; set; }
        bool IsValid { get; set; }
        Task LoadAsync();
        bool CanExecuteLogin();
        void ExecuteLogin();
        event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = null);
    }
}