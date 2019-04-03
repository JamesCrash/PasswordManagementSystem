using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashPasswordSystem.UI.ViewModels
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int id);
        bool HasChanges { get; }
        int Id { get; }
    }
}
