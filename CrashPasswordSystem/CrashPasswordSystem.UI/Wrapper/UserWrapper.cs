using CrashPasswordSystem.Models;
using System.Collections.Generic;

namespace CrashPasswordSystem.UI.Wrapper
{
    public class UserWrapper : ModelWrapper<User>
    {
        public UserWrapper(User model) : base(model)
        {
        }

        public int UserId => Model.UserID;

        public string Password
        {
            get; //=> GetValue<string>();
            set; //=> SetValue(value);
        }

        public string UserFirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string UserLastName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string UserInitials
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string UserEmail
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string UserHash
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string UserSalt
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public System.DateTime? UserDateCreated { get; set; }

        public bool? UserActive
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(UserFirstName):
                    if(string.IsNullOrWhiteSpace(UserFirstName))
                    {
                        yield return "Robots are not valid friends";
                    }
                    break;
            }
        }
    }
}
