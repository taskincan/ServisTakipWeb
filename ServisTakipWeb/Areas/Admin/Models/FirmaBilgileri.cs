using ServisTakipWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServisTakipWeb.Areas.Admin.Models
{
    public class FirmaBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Firma Kodu")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(9)]
        [MaxLength(10)]
        public string FirmaKodu { get; set; }

        [Display(Name = "Firma Adı")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(5)]
        [MaxLength(50)]
        public string FirmaAdi { get; set; }

        [Display(Name = "Yetkili Kişi")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string YetkiliKisi { get; set; }

        [Display(Name = "Cep Telefonu")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(10)]
        [MaxLength(11)]
        public string Gsm { get; set; }

        [Display(Name = "Firma Telefonu")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(10)]
        [MaxLength(11)]
        public string FirmaTel { get; set; }

        [Display(Name = "Web Sitesi")] 
        [DataType(DataType.Text)] 
        [MaxLength(50)]
        public string WebSite { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(3)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Display(Name = "Şifre")] 
        [DataType(DataType.Password)]
        [MinLength(3)]
        [MaxLength(50)]
        public string Password2 { get; set; }

        [Display(Name = "Adres")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Adres { get; set; }
        
        [Display(Name = "E-mail")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(8)]
        [MaxLength(40)]
        public string Email { get; set; }
         
        public int AdminID { get; set; }

        public static List<FirmaBilgileri> firmaList = new List<FirmaBilgileri>();
    }
}