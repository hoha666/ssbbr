using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ssbbr.Data.DataModels
{
    public class illegal_contents
    {
        public long id { get; set; }
        public int game_id { get; set; }
        [MaxLength(50)]
        public string type { get; set; }
        [Display(Name ="موارد محتوایی")]
        public string comment { get; set; }
        [MaxLength(50)]
        public string url { get; set; }
    }
}
