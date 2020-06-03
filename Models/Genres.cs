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
        [RegularExpression(@"^[А-ЯІЇЄа-яіїєA-Za-z'-'' ']*$", ErrorMessage = "Введена некоректна назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Опис")]
        [RegularExpression(@"^[А-ЯІЇЄа-яіїєA-Za-z'-'' ']*$", ErrorMessage = "Введена некоректний опис")]
        public string Description { get; set; }

        public virtual ICollection<Games> Games { get; set; }
    }
}
