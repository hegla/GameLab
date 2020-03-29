using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sem2Lab1SQLServer
{
    public partial class Games
    {
        public Games()
        {
            Publications = new HashSet<Publications>();
            Ratings = new HashSet<Ratings>();
        }
        [Display(Name = "ID")]
        public int GameId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        [RegularExpression(@"^[А-ЯІЄЇа-яіїєA-Za-z'-'' ']*$", ErrorMessage = "Введена некоректна назва")]
        public string Name { get; set; }
        [Display(Name = "Розробник")]
        public int DeveloperId { get; set; }
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Бюджет, $")]
        [Column(TypeName = "decimal(18, 0)")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Введіть додатне число, яке не починається нулем")]
        public double Budget { get; set; }

        [Display(Name = "Розробник")]
        public virtual Developers Developer { get; set; }
        [Display(Name = "Жанр")]
        public virtual Genres Genre { get; set; }
        public virtual ICollection<Publications> Publications { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
