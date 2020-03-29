using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Critics
    {
        public Critics()
        {
            Ratings = new HashSet<Ratings>();
        }

        [Display(Name = "Id")]
        public int CriticId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Ім'я користувача")]
        [RegularExpression(@"^[A-Za-z'_']*$", ErrorMessage = "Введена некоректне ім'я")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Деталі")]
        [RegularExpression(@"^[А-ЯІЇЄа-яіїєA-Za-z' ']*$", ErrorMessage = "Введена некоректний опис")]
        public string Description { get; set; }

        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
