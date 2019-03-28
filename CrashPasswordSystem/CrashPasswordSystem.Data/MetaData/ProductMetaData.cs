using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrashPasswordSystem.Data
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        [NotMapped]
        public string Password { get; set; }

        static User()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(User), typeof(UserMetaData)), typeof(User));
        }
    }
    public class UserMetaData
    {
        //[Required(ErrorMessage = "Password is a required.")]
        //public string UserHash { get; set; }

        [EmailAddress(ErrorMessage = "Not a valid email address.")]
        public string UserEmail { get; set; }

    
        [Required(ErrorMessage = "Password is a required.")]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", 
          //  ErrorMessage = "Password must be a minimum of eight characters, have at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string Password { get; set; }

    }
}
