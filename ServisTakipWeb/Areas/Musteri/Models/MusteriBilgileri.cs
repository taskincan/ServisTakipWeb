using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.Musteri.Models
{
    public class MusteriBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int CreateUserID { get; set; }

        [Display(Name = "Müşteri Kodu")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string MusteriKodu { get; set; }

        [Display(Name = "Müşteri Adı")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string MusteriAdi { get; set; }

        [Display(Name = "Adres")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Adres { get; set; }

        [Display(Name = "Şifre")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [MinLength(1)]
        [MaxLength(20)]
        public string Password2 { get; set; }

        [Display(Name = "Yetkili Kişi")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string YetkiliKisi { get; set; }

        [Display(Name = "Telefon")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Tel1 { get; set; }

        [Display(Name = "Telefon-2")]
        [DataType(DataType.Text)]
        [MinLength(11)]
        [MaxLength(11)]
        public string Tel2 { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(40)]
        public string Email { get; set; }

        [Display(Name = "Vergi Dairesi")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string VergiDairesi { get; set; }

        [Display(Name = "Vergi Numarası")]
        [DataType(DataType.Text)]
        [MaxLength(15)]
        public string VergiNumarasi { get; set; }

        public static List<MusteriBilgileri> musteriList = new List<MusteriBilgileri>();
    }
}