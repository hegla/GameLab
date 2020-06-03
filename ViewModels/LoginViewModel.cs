using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sem2Lab1SQLServer.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введена некоректна електронна адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
