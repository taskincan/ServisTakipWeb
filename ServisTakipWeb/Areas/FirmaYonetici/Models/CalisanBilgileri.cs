﻿using ServisTakipWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServisTakipWeb.Areas.FirmaYonetici.Models
{
    public class CalisanBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int FcID { get; set; }

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

        [Display(Name = "Firma ID")]
        [DataType(DataType.Text)]
        [Required]
        public int FirmaID { get; set; }

        [Display(Name = "Create User ID")]
        [DataType(DataType.Text)]
        [Required]
        public int CreateUserID { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [MinLength(1)]
        [MaxLength(20)]
        public string Password2 { get; set; }


        [Display(Name = "Atanacak Çalışan")]
        public string CalisanAdSoyad { get; set; }

        public static List<CalisanBilgileri> firmaCalisanList = new List<CalisanBilgileri>();
    }
}