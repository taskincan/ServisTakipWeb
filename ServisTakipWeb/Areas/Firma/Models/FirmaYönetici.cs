using ServisTakipWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServisTakipWeb.Areas.Firma.Models
{
    public class FirmaYönetici : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int FyID { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
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

        [Display(Name = "Firma ID")]
        [DataType(DataType.Text)]
        [Required] 
        public int FirmaID { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [MinLength(1)]
        [MaxLength(20)]
        public string YoneteciPassword2 { get; set; } 

        public static List<FirmaYönetici> firmaYoneticiList = new List<FirmaYönetici>();
    }
}