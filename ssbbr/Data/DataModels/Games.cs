using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ssbbr.Data.DataModels
{
    public class Games
    {
        public int id { get; set; }
        [Display(Name = "کد")]
        public string code { get; set; }
        [Display(Name = "عنوان بازی")]
        [Required]
        public string name { get; set; }
        [Display(Name = "پلتفرم های بازی")]
        [Required]
        public string platform { get; set; }
        [Display(Name = "ناشر مجاز")] // dge injoori shod ke majboor shodimaaa alaki maselan
        public string type_of_source { get; set; }
        [Display(Name = "رده بندی سنی")]
        public string age_rating { get; set; }
        [Display(Name = "نوع ممنوعیت")]
        public string genre1 { get; set; }
        [Display(Name = "نشان رنگی بازی")]
        public string genre2 { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreatedDate { get; set; }
    }
}
