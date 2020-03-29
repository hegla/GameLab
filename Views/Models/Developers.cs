using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Developers
    {
        public Developers()
        {
            Games = new HashSet<Games>();
        }
        public int DeveloperId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Дата")]
        public DateTime FoundationDate { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Кількість працівників")]
        public int WorkersNumber { get; set; }
        [Display(Name = "Країна")]
        public int CountryId { get; set; }
        [Display(Name = "Країна")]

        public virtual Countries Country { get; set; }
        public virtual ICollection<Games> Games { get; set; }
    }
}
