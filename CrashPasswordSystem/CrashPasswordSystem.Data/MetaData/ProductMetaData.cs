using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrashPasswordSystem.Data.MetaData
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        static User()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(User), typeof(UserMetaData)), typeof(User));
        }
    }
    public class UserMetaData
    {
        [Required(ErrorMessage = "Password is a required.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must be a minimum of eight characters, have at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string UserEmail { get; set; }

    }
}
