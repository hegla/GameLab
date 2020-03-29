using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Ratings
    {
        public int RatingId { get; set; }
        [Display(Name = "Критик")]
        public int CriticId { get; set; }
        [Display(Name = "Гра")]
        public int GameId { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [Display(Name = "Оцінка")]
        public int Mark { get; set; }

        [Display(Name = "Критик")]
        public virtual Critics Critic { get; set; }
        [Display(Name = "Оцінка")]
        public virtual Games Game { get; set; }
    }
}
