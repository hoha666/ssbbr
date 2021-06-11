using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ssbbr.Data.DataModels
{
    public class genres
    {
        [MaxLength(50)]
        public string genre { get; set; }
    }
}
