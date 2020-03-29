using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sem2Lab1SQLServer
{
    public partial class Publications
    {
        public int PublicationId { get; set; }
        [Display(Name = "Гра")]
        public int GameId { get; set; }
        [Display(Name = "Видавництво")]
        public int PublisherId { get; set; }
        [Display(Name = "Гра")]
        public virtual Games Game { get; set; }
        [Display(Name = "Видавництво")]
        public virtual Publishers Publisher { get; set; }
    }
}
