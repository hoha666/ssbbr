using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ssbbr.Data
{
    public class RegisterForm
    {
        [Key]
        [Required(ErrorMessage = "الزامی می باشد")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [Column("نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [Column("نام خانوادگی")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 کارکتر باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "کد ملی باید عدد باشد")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = " باید 10 کارکتر باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "باید عدد باشد")]
        [Column("موبایل")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "باید عدد باشد")]
        [Column("تلفن ثابت")]
        public string Phone { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "باید عدد باشد")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [DataType(DataType.EmailAddress, ErrorMessage = "لطفا ایمیل را صحیح وارد کنید.")]
        [Column("ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [Column("استان")]
        public string Province { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        [Column("شهر")]
        public string City { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        public int Resume { get; set; }

        [Required(ErrorMessage = "الزامی می باشد")]
        public string Responsibility { get; set; }

        [Required(ErrorMessage = "کلمه رمز الزامی می باشد")]
        [StringLength(255, ErrorMessage = "کلمه رمز باید بین 8 تا 255 کاراکتر باشد", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "کلمه رمز الزامی می باشد")]
        [StringLength(255, ErrorMessage = "کلمه رمز باید بین 8 تا 255 کاراکتر باشد", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }
    }
}
