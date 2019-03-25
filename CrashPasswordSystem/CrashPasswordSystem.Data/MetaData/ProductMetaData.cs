using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrashPasswordSystem.Data.MetaData
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        static Product()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Product), typeof(ProductMetaData)), typeof(Product));
        }
    }
    public class ProductMetaData
    {
        [Required(ErrorMessage = "Product Description is required.")]
        public string ProductDescription { get; set; }
    }
}
