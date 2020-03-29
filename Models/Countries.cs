using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Countries
    {
        public Countries()
        {
            Developers = new HashSet<Developers>();
        }

        [Display(Name = "ID")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Назва")]
        [RegularExpression(@"^[А-ЯІЄЇа-яіїє'-'' ']*$", ErrorMessage = "Введена некоректна назва")]
        public string Name { get; set; }
        [Display(Name = "Континент")]
        public int ContinentId { get; set; }
        [Display(Name = "Континент")]
        public virtual Continents Continent { get; set; }
        public virtual ICollection<Developers> Developers { get; set; }
    }
}
