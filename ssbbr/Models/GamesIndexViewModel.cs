using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ssbbr.Data.DataModels;

namespace ssbbr.Models
{
    public class GamesIndexViewModel
    {
        public int PageNumber { get; set; }
        public int RecordCount { get; set; }
        public int pageCount { get; set; }
        public string keyword { get; set; }
        public string searchStatus { get; set; }        
        public bool PrimaryLoad { get; set; }
        public IEnumerable<ssbbr.Data.DataModels.Games> DataStash { get; set; }
        public GamesIndexViewModel()
        {
            PrimaryLoad = true;
        }
    }
}
