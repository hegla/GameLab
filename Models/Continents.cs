using Sem2Lab1SQLServer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Continents
    {
        public Continents()
        {
            Countries = new HashSet<Countries>();
        }

        [Display(Name = "ID")]
        public int ContinentId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Назва")]
        [ValidСontinetName(ErrorMessage ="Введено недопустимий континент")]
        [RegularExpression(@"^[А-ЯІЇЄа-яіїє' ']*$", ErrorMessage = "Введена некоректна назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Площа, млн км^2")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Введіть додатне число, яке не починається нулем")]
        public int Area { get; set; }

        public virtual ICollection<Countries> Countries { get; set; }
    }
}
