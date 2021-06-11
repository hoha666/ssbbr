using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ssbbr.Models
{
    public class AddGameViewModel
    {
        [Display(Name = "عنوان بازی")]
        public string name { get; set; }

        [Display(Name = "عنوان خلاصه")]
        public string nickName { get; set; }


        [Display(Name = "PC")]
        public bool PC { get; set; }
        public bool PS1 { get; set; }
        public bool PS2 { get; set; }
        public bool PS3 { get; set; }
        public bool PS4 { get; set; }
        public bool Xbox360 { get; set; }
        public bool XboxOne { get; set; }
        public bool Android { get; set; }
        public bool iOS { get; set; }
        public bool Wii { get; set; }
        public bool MAC { get; set; }
        public bool NintendoSwitch { get; set; }

        [Display(Name = "نوع ممنوعیت")]
        public string typeOfFiltering { get; set; }
        [Display(Name = "ناشر مجاز")]
        public string publisherName { get; set; }
        [Display(Name = "رده بندی سنی")]
        public string age { get; set; }

        [Display(Name = "موسیقی ناهنجار / موسیقی دارای خواننده زن")]
        public bool na_music { get; set; }
        [Display(Name = "گفتار ناشایست شدید و به دفعات")]
        public bool na_badLanguage { get; set; }
        [Display(Name = "رقص")]
        public bool na_dance { get; set; }
        [Display(Name = "رفتارهای غیر مؤدبانه")]
        public bool na_badBehave { get; set; }
        [Display(Name = "ترویج مردم آزاری")]
        public bool na_publicDamage { get; set; }

        [Display(Name = "وجود ابزار قمار در مکان های مرتبط با قمار")]
        public bool bet_inPlace { get; set; }
        [Display(Name = "انجام قمار واقعی / غیر واقعی، توسط شخصیت اصلی در بازی")]
        public bool bet_doneByYou { get; set; }

        [Display(Name = "استفاده / اثر استفاده از مشروبات الکلی")]
        public bool alch_use { get; set; }
        [Display(Name = "نمایش مکان های مرتبط با مشروبات الکلی ) Bar, Casino )")]
        public bool alch_show { get; set; }

        [Display(Name = "بوسیدن")]
        public bool erotic_kiss { get; set; }
        [Display(Name = "کلام / نوشته / نشانه مربوط به محرک های جنسی")]
        public bool erotic_symbol { get; set; }
        [Display(Name = "رابطه جنسی")]
        public bool erotic_porn { get; set; }
        [Display(Name = "مفهوم رابطه جنسی")]
        public bool erotic_content { get; set; }
        [Display(Name = "مکان های مربوط به رابطه جنسی یا تفریحات جنسی")]
        public bool erotic_place { get; set; }
        [Display(Name = "عریانی")]
        public bool erotic_nude { get; set; }
        [Display(Name = "نیمه عریانی")]
        public bool erotic_halfNude { get; set; }
        [Display(Name = "اغواگری / لمس جنسی")]
        public bool erotic_acts { get; set; }
        [Display(Name = "انحرافات جنسی")]
        public bool erotic_problems { get; set; }
        [Display(Name = "رقص جنسی")]
        public bool erotic_dance { get; set; }


        [Display(Name = "نمایش صحنه های قساوت آمیز به صورت مکرر یا خشونت شدید در بازی")]
        public bool violence_show { get; set; }
        [Display(Name = "خشونت شدید همراه با ترس و ایجاد استرس و اضطراب زیاد در بازی")]
        public bool violence_extreme { get; set; }

        [Display(Name = "استفاده / اثر استفاده از مواد مخدر )شامل مصرف، کسب درآمد، ترویج استفاده، کم رنگ کردن تابوی موادمخدر(")]
        public bool drugs { get; set; }


        [Display(Name = "هتک حرمت مقدسات")]
        public bool Islam_places { get; set; }
        [Display(Name = "اهانت و تبعیض علیه مسلمانان")]
        public bool Islam_racist { get; set; }
        [Display(Name = "اهانت و تبعیض علیه ایرانیان")]
        public bool Islam_racistIranian { get; set; }
        [Display(Name = "ترویج کفر و شرک")]
        public bool Islam_blasphemy { get; set; }
        [Display(Name = "ترویج رذایل اخلاقی")]
        public bool Islam_attitude { get; set; }
        [Display(Name = "خالکوبی کردن کاراکتر ها توسط بازیکن")]
        public bool Islam_tatto { get; set; }




        public IFormFile ImageUpload1 { get; set; }

        public IFormFile ImageUpload2 { get; set; }

        public IFormFile ImageUpload3 { get; set; }

        public IFormFile ImageUpload4 { get; set; }

        public IFormFile ImageUpload5 { get; set; }

        public IFormFile ImageUpload6 { get; set; }
    }

}
