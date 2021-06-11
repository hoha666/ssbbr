using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ssbbr.Models
{
    public class HomeViewModel
    {
        [Display(Name = "keyword")]
        public string keyword { get; set; }
        public List<searchResult> Results { get; set; }
        public string searchStatus { get; set; }
    }
    public class searchResult
    {
        public int rowNumber { get; set; }
        public string gameName { get; set; }
        public string platform { get; set; }
        public string status { get; set; }
        public string publisher { get; set; }
        public string age { get; set; }
        public string date { get; set; }
    }
}
