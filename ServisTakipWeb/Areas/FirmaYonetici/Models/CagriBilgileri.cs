using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.FirmaYonetici.Models
{
    public class CagriBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int CreateUserID { get; set; }

        [Display(Name = "Çağrı Kodu")]
        [Required]
        public int CagriNo { get; set; }

        [Display(Name = "Müşteri Adı")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string MusteriAdi { get; set; }

        [Display(Name = "Müşteri Kodu")]
        [DataType(DataType.Text)]
        [Required] 
        public string MusteriKodu { get; set; }

        [Display(Name = "Sözleşme")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string Sozlesme { get; set; }

        [Display(Name = "Çağrı Açılış Tarihi")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CagriAcilisTarihi { get; set; }

        [Display(Name = "İlgili Kişi")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string IlgiliKisi { get; set; }

        [Display(Name = "Adres")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Adres { get; set; }

        [Display(Name = "Telefon No")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(11)]
        public string Telefon { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.Text)]
        [MaxLength(40)]
        public string Email { get; set; }

        [Display(Name = "Cihaz Tipi")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string CihazTipi { get; set; }

        [Display(Name = "Marka")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Marka { get; set; }

        [Display(Name = "Model")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Display(Name = "Seri No")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string SeriNo { get; set; }

        [Display(Name = "Barkod No")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string BarkodNo { get; set; }

        [Display(Name = "Açıklama")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(100)]
        public string Aciklama { get; set; }

        [Display(Name = "Çağrı Detayı")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(300)]
        public string CagriDetayi { get; set; }

        [Display(Name = "İşlem Gördü Mü")]
        [Required]
        public bool IslemGorduMu { get; set; }

        [Display(Name = "Sarf Malzeme Talebi")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string SarfMalzemeTalebi { get; set; }

        [Display(Name = "Durum")]
        [DataType(DataType.Text)] 
        public string Durum { get; set; }

        public static List<CagriBilgileri> cagriList = new List<CagriBilgileri>();
    }
}