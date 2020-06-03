using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sem2Lab1SQLServer.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Поле необхідно заповнити")]
        [RegularExpression(@"^[А-ЯІЄЇа-яіїєA-Za-z'-'' ']*$", ErrorMessage = "Введене некоректна назва")]
        [Display(Name = "Назва")]
        public string Name { get; set; }
    }
}
