using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ssbbr.Data.DataModels;

namespace ssbbr.Models
{
    public class GameDetailsViewModel : Games
    {
        public int pagenumber { get; set; }
        public illegal_contents ic { get; set; }
        public List<string> files { get; set; }
    }
}
