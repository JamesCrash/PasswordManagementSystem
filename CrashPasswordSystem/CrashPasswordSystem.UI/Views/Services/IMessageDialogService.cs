using System.Threading.Tasks;
using MahApps.Metro.Controls;

namespace CrashPasswordSystem.UI.Views.Services
{
    public interface IMessageDialogService
    {
        MetroWindow MetroWindow { get; }
        Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);
        Task ShowInfoDialogAsync(string text);
    }
}