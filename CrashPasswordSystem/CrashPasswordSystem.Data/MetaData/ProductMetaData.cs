using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrashPasswordSystem.Data
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
        public string UserHash { get; set; }

        [EmailAddress(ErrorMessage = "Not a valid email address.")]
        public string UserEmail { get; set; }

    }
}
