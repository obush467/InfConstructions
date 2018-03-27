using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfConstractions.Models
{
    [MetadataType(typeof(proverkaGUMetadata))]
    public partial class proverkaGU
    {
        internal sealed class proverkaGUMetadata
        {
            public string Link { get { return "\\\\moscow.gbumac.ru\\МАЦ\\Мониторинг\\Каталог_ГУ\\Проверка_паспортов_ГУ\\Документы\\" + Num; } }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым")]
            public string Num { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public string Okrug { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public string Raion { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public string Street { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Значение не может быть пустым. Выберите из списка.")]
            public string Dom { get; set; }
            public string ErrorAdress { get; set; }
            public string Fakt { get; set; }
            public Nullable<bool> Maket { get; set; }
            public Nullable<bool> Passport { get; set; }
            public string Prim { get; set; }
            public Nullable<bool> Checking { get; set; }
            [Key]
            public int ID { get; set; }
            public string Plash { get; set; }
            [Timestamp]
            public byte[] ts { get; set; }
            public Nullable<System.DateTimeOffset> updated { get; set; }

        }
    }
}
