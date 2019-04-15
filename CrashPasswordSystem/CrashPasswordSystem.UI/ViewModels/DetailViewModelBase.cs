using CrashPasswordSystem.UI.Views.Services;
using Prism.Events;
using System.Threading.Tasks;

namespace CrashPasswordSystem.UI.ViewModels
{
    public abstract class DetailViewModelBase : ViewModelBase
    {
        private bool _hasChanges;
        protected readonly IMessageDialogService MessageDialogService;
        private int _id;
        private string _title;

        public DetailViewModelBase(IEventAggregator eventAggregator,
          IMessageDialogService messageDialogService)
        {
            EventAggregator = eventAggregator;
            MessageDialogService = messageDialogService;
        }

        public abstract Task LoadAsync(int id);
        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            protected set => base.SetProperty(ref _title, value);
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    SetProperty(ref _hasChanges, value);
                }
            }
        }

        protected abstract void OnDeleteExecute();

        protected abstract bool OnSaveCanExecute();

        protected abstract void OnSaveExecute();

        protected async virtual void OnCloseDetailViewExecute()
        {
            if (HasChanges)
            {
                var result = await MessageDialogService.ShowOkCancelDialogAsync(
                  "You've made changes. Close this item?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            //EventAggregator.GetEvent<AfterDetailClosedEvent>()
            //  .Publish(new AfterDetailClosedEventArgs
            //  {
            //      Id = this.Id,
            //      ViewModelName = this.GetType().Name
            //  });
        }

        //protected async Task SaveWithOptimisticConcurrencyAsync(Func<Task> saveFunc,
        //  Action afterSaveAction)
        //{
        //    try
        //    {
        //        await saveFunc();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        var databaseValues = ex.Entries.Single().GetDatabaseValues();
        //        if (databaseValues == null)
        //        {
        //            await MessageDialogService.ShowInfoDialogAsync("The entity has been deleted by another user");
        //            RaiseDetailDeletedEvent(Id);
        //            return;
        //        }

        //        var result = await MessageDialogService.ShowOkCancelDialogAsync("The entity has been changed in "
        //         + "the meantime by someone else. Click OK to save your changes anyway, click Cancel "
        //         + "to reload the entity from the database.", "Question");

        //        if (result == MessageDialogResult.OK)
        //        {
        //            // Update the original values with database-values
        //            var entry = ex.Entries.Single();
        //            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
        //            await saveFunc();
        //        }
        //        else
        //        {
        //            // Reload entity from database
        //            await ex.Entries.Single().ReloadAsync();
        //            await LoadAsync(Id);
        //        }
        //    };

        //    afterSaveAction();
        //}

    }
}
