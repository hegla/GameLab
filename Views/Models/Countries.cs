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
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Континент")]
        public int ContinentId { get; set; }

       // public virtual Continents Continent { get; set; }
        public virtual ICollection<Developers> Developers { get; set; }
    }
}
