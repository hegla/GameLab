using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sem2Lab1SQLServer.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введена некоректна електронна адреса")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Ім'я")]
        [RegularExpression(@"^[А-ЯІЄЇа-яіїєA-Za-z'-'' ']*$", ErrorMessage = "Введене некоректне ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Прізвище")]
        [RegularExpression(@"^[А-ЯІЄЇа-яіїєA-Za-z'-'' ']*$", ErrorMessage = "Введене некоректне прізвище")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Display(Name = "Підтвердження паролю")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
