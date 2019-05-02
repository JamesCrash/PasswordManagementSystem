using CrashPasswordSystem.UI.ViewModels;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrashPasswordSystem.Core;
using System.Collections.ObjectModel;

namespace CrashPasswordSystem.UI.Wrapper
{
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorsByPropertyName
         = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private ObservableCollection<string> _errors;
        public ObservableCollection<string> Errors
        {
            get => _errors;
        }

        public NotifyDataErrorInfoBase()
        {
            _errors = new ObservableCollection<string>();
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName)
              ? _errorsByPropertyName[propertyName]
              : null;
        }

        public void SetErrors(Dictionary<string, List<string>> value)
        {
            _errorsByPropertyName = value;

            OnErrorsChanged();
        }

        protected virtual void OnErrorsChanged(string propertyName = null)
        {
            _errors.Clear();

            _errorsByPropertyName.Values.SelectMany(list => list)
                                 .ToList()
                                 .ForEach(item => _errors.Add(item));

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
