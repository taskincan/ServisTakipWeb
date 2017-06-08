using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.Musteri.Models
{
    public class MusteriCalisanBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int McID { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Display(Name = "Kullanılan Şifre")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Display(Name = "Yeni Şifre")] 
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(20)]
        public string PasswordYeni { get; set; }
  
        [Display(Name = "Müşteri ID")]
        [Required]
        public int MusteriID { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

        public static List<MusteriCalisanBilgileri> musteriCalisanList = new List<MusteriCalisanBilgileri>();
    }
}