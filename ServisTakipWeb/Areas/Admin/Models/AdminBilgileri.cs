using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServisTakipWeb.Areas.Admin.Models
{
    public class AdminBilgileri
    {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)] 
            public int ID { get; set; }

            [Display(Name = "Kullanıcı Adı")]
            [DataType(DataType.Text)]
            [Required]
            [MinLength(1)]
            [MaxLength(30)]
            public string UserName { get; set; }

            [Display(Name = "Şifre")]
            [Required]
            [DataType(DataType.Password)]
            [MinLength(1)]
            [MaxLength(20)]
            public string Password { get; set; }

            [Display(Name = "Kayıt Tarihi")]
            public DateTime CreateDate { get; set; }

            public static List<AdminBilgileri> adminList = new List<AdminBilgileri>();
    }

}