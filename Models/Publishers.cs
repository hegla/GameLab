using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sem2Lab1SQLServer
{
    public partial class Publishers
    {
        public Publishers()
        {
            Publications = new HashSet<Publications>();
        }

        [Display(Name = "Id")]
        public int PublisherId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Назва")]
        [RegularExpression(@"^[А-ЯІЇЄа-яіїєA-Za-z'-'' ']*$", ErrorMessage ="Введена некоректна назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Дохід, $")]
        [Column(TypeName = "decimal(18, 0)")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage ="Введіть додатне число, яке не починається нулем")]
        public double Earnings { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Електронна адреса")]
        [EmailAddress(ErrorMessage ="Введена некоректна електронна адреса")]
        public string Contacts { get; set; }

        public virtual ICollection<Publications> Publications { get; set; }
    }
}
