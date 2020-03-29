using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Publishers
    {
        public Publishers()
        {
            Publications = new HashSet<Publications>();
        }

        [Display(Name = "Id")]
        public int PublisherId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Дохід")]
        public decimal Earnings { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Контакти")]
        public string Contacts { get; set; }

        public virtual ICollection<Publications> Publications { get; set; }
    }
}
