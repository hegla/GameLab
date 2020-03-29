using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Genres
    {
        public Genres()
        {
            Games = new HashSet<Games>();
        }
        [Display(Name = "ID")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        public virtual ICollection<Games> Games { get; set; }
    }
}
