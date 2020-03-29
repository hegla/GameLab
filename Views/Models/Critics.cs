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
        [Display(Name = "Ім'я")]
        public string Username { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
