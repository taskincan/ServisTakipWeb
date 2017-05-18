using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.Musteri.Models
{
    public class MusteriYoneticiBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MyID { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Display(Name = "Ad")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(20)]
        public string Soyad { get; set; }

        [Display(Name = "Gsm")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(11)]
        public string Gsm { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(40)]
        public string Email { get; set; }

        [Display(Name = "Müşteri ID")]
        [Required]
        public int MusteriID { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [MinLength(1)]
        [MaxLength(20)]
        public string Password2 { get; set; }

        public static List<MusteriYoneticiBilgileri> musteriYoneticiList = new List<MusteriYoneticiBilgileri>();
    }
}