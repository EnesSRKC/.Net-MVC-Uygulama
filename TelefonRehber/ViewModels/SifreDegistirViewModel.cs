using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelefonRehber.ViewModels
{
    public class SifreDegistirViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Eski Şifre :")]
        [Required(ErrorMessage = "Lütfen eski şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string SifreEski { get; set; }

        [Display(Name = "Yeni Şifre :")]
        [Required(ErrorMessage = "Lütfen yeni şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string SifreYeni { get; set; }

        [Display(Name = "Yeni Şifre Tekrar :")]
        [Required(ErrorMessage = "Lütfen yeni şifrenizi tekrar giriniz.")]
        [DataType(DataType.Password)]
        public string SifreYeniOnay { get; set; }

    }
}