using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServisTakipWeb.Models
{
    public class Giris
    {
        [Display(Name = "Kullanici Adi")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Display(Name = "Sifre")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(1)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}