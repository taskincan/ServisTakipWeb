using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.FirmaCalisan.Models
{
    public class CagriBekletBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int BekletmeyeAlanCalisanID { get; set; }

        public int AtayanID { get; set; }

        [Display(Name = "Çağrı Kodu")]
        public int CagriNo { get; set; }

        [Display(Name = "Müşteri Adı")]
        public string MusteriAdi { get; set; }

        [Display(Name = "Müşteri Kodu")]
        public string MusteriKodu { get; set; }

        [Display(Name = "Çağrı Açılış Tarihi")]
        public DateTime CagriAcilisTarihi { get; set; }

        [Display(Name = "İlgili Kişi")]
        public string IlgiliKisi { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [Display(Name = "Telefon No")]
        public string Telefon { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Cihaz Tipi")]
        public string CihazTipi { get; set; }

        [Display(Name = "Marka")]
        public string Marka { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Seri No")]
        public string SeriNo { get; set; }

        [Display(Name = "Barkod No")]
        public string BarkodNo { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Çağrı Detayı")]
        public string CagriDetayi { get; set; }

        [Display(Name = "Sarf Malzeme Talebi")]
        public string SarfMalzemeTalebi { get; set; }

        [Display(Name = "Acil")]
        public bool AcilMi { get; set; }

        [Display(Name = "Yönetici Notu")]
        public string Not { get; set; }

        [Display(Name = "Çağrı İptal Etme Nedeni")]
        [DataType(DataType.Text)]
        [Required]
        [MaxLength(200)]
        public string CagriBekletmeNedeni { get; set; } 
    }
}