using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using InfConstractions.Properties;
using System.Windows;
using System.ComponentModel;

namespace InfConstractions.Models
{
    [MetadataType(typeof(GUPassport_SiteMetadata))]
    public partial class GUPassport_Site
    {
        public Action<object, PropertyChangedEventArgs> PropertyChanged { get; internal set; }

        internal sealed class GUPassport_SiteMetadata
        {
            public System.Guid id { get; set; }
            public System.Guid GUPassport_ID { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public short Block_Number { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public short Row_Number { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public string Site_Number { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public string Content_Text { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым")]
            public string Content_Transliteration { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым")]
            public string Content_Address { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public string Content_Direction { get; set; }
            public Nullable<System.Guid> Object_ID { get; set; }
            public Nullable<System.Guid> HouseID { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым")]
            public string Minutes { get; set; }
            public byte[] tsc { get; set; }
        }

        public class AtLeastChooseOneItem : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            { return new ValidationResult("fsdfsdfsdfsd");
            }
                }

    }
}
