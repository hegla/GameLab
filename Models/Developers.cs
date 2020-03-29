using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sem2Lab1SQLServer
{
    public partial class Developers :IValidatableObject
    {
        public Developers()
        {
            Games = new HashSet<Games>();
        }
        public int DeveloperId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Назва")]
        [RegularExpression(@"^[А-ЯІЄЇа-яіїєA-Za-z'-'' ']*$", ErrorMessage = "Введена некоректна назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime FoundationDate { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Кількість працівників")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Введіть додатне число, яке не починається нулем")]
        public int WorkersNumber { get; set; }
        [Display(Name = "Країна")]
        public int CountryId { get; set; }
        [Display(Name = "Країна")]

        public virtual Countries Country { get; set; }
        public virtual ICollection<Games> Games { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            long ticks = new DateTime(1900, 01, 01,
    new CultureInfo("ua-UA", false).Calendar).Ticks;
            DateTime date = new DateTime(ticks);

            if (FoundationDate > DateTime.Today || FoundationDate < date)
            {
                results.Add(new ValidationResult("Дата має бути між 01.01.1900 та " + DateTime.Today.ToShortDateString(), new[] { "FoundationDate" }));
            }

            return results;
        }
    }
}
