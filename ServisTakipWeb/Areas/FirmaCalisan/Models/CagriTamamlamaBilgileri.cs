using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.FirmaCalisan.Models
{
    public class CagriTamamlamaBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TamamlananID { get; set; }

        public int TamamlayanYoneticiID { get; set; }
        public int TamamlayanCalisanID { get; set; }
        public bool MusteriSozlesmeParcaDahilMi { get; set; }

        [Display(Name = "Form No")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string FormNo { get; set; }

        [Display(Name = "Müşteri Adı")]
        public string MusteriAdi { get; set; }

        [Display(Name = "Tamamlayan Kişi")]
        public string TamamlayanKisi { get; set; }

        [Display(Name = "Yetkili Kişi")]
        public string YetkiliKisi { get; set; }

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Müşteri Kodu")]
        public string MusteriKodu { get; set; }

        [Display(Name = "Müşteri ID")]
        public int MusteriID { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [Display(Name = "Vergi Dairesi")]
        public string VergiDairesi { get; set; }

        [Display(Name = "Vergi Numarası")]
        public string VergiNumarasi { get; set; }

        [Display(Name = "Bildirilen Arıza")]
        public string BildirilenAriza { get; set; }

        [Display(Name = "Hizmet Tipi")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string HizmetTipi { get; set; }

        [Display(Name = "Hizmet Tipi")]
        [DataType(DataType.Text)] 
        [MinLength(1)]
        [MaxLength(30)]
        public string HizmetTipi2 { get; set; }

        [Display(Name = "Cihazın Hizmet Durumu")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string CihazinHizmetDurumu { get; set; }

        [Display(Name = "Cihazın Hizmet Durumu")]
        [DataType(DataType.Text)] 
        [MinLength(1)]
        [MaxLength(30)]
        public string CihazinHizmetDurumu2 { get; set; }

        [Display(Name = "Çağrının Bildirildiği Tarih")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CagriBildirildigiTarih { get; set; }

        [Display(Name = "Hizmet Başlangıç Tarihi")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HizmetBaslangicTarihi { get; set; }

        [Display(Name = "Hizmet Bitiş Tarihi")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HizmetBitisTarihi { get; set; }

        [Display(Name = "Çağrı Kayıt No")]
        public int CagriKayitNo { get; set; }

        [Display(Name = "Mesai Saati İçinde")]
        [Required]
        public bool MesaiSaatiIcindeMi { get; set; }

        [Display(Name = "Yapılan İşin Açıklaması")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(400)]
        public string YapilanIsinAciklamasi { get; set; }

        [Display(Name = "Sonuç")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(20)]
        public string Sonuc { get; set; }

        [Display(Name = "Marka-1")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Marka1 { get; set; }

        [Display(Name = "Model-1")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Model1 { get; set; }

        [Display(Name = "Seri No-1")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string SeriNo1 { get; set; }

        [Display(Name = "Marka-2")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Marka2 { get; set; }

        [Display(Name = "Model-2")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Model2 { get; set; }

        [Display(Name = "Seri No-2")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string SeriNo2 { get; set; }

        [Display(Name = "Marka-3")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Marka3 { get; set; }

        [Display(Name = "Model-3")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Model3 { get; set; }

        [Display(Name = "Seri No-3")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string SeriNo3 { get; set; }

        [Display(Name = "Marka-4")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Marka4 { get; set; }

        [Display(Name = "Model-4")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string Model4 { get; set; }

        [Display(Name = "Seri No-4")]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(30)]
        public string SeriNo4 { get; set; }

        [Display(Name = "Parça Adı")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ParcaAdi1 { get; set; }

        [Display(Name = "Parça No")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ParcaNo1 { get; set; }

        [Display(Name = "Miktar")]
        public int Miktar1 { get; set; }

        [Display(Name = "Birim Fiyatı")]
        public double BirimFiyati1 { get; set; }


        [Display(Name = "Parça Adı")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ParcaAdi2 { get; set; }

        [Display(Name = "Parça No")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ParcaNo2 { get; set; }

        [Display(Name = "Miktar")]
        public int Miktar2 { get; set; }

        [Display(Name = "Birim Fiyatı")]
        public double BirimFiyati2 { get; set; }


        [Display(Name = "Parça Adı")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ParcaAdi3 { get; set; }

        [Display(Name = "Parça No")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ParcaNo3 { get; set; }

        [Display(Name = "Miktar")]
        public int Miktar3 { get; set; }

        [Display(Name = "Birim Fiyatı")]
        public double BirimFiyati3 { get; set; }

        [Display(Name = "Süre")]
        public int Sure1 { get; set; }

        [Display(Name = "Birim Fiyatı")]
        public double BirimFiyatiIscilik1 { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        public string AciklamaIscilik1 { get; set; }

        [Display(Name = "Süre")]
        public int Sure2 { get; set; }

        [Display(Name = "Birim Fiyatı")]
        public double BirimFiyatiIscilik2 { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        public string AciklamaIscilik2 { get; set; }

        [Display(Name = "Süre")]
        public int Sure3 { get; set; }

        [Display(Name = "Birim Fiyatı")]
        public double BirimFiyatiIscilik3 { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        public string AciklamaIscilik3 { get; set; }

        [Required]
        public bool AnketYapildiMi { get; set; }

        [Display(Name = "Anket")]
        public string AnketYapildiMiTablo { get; set; }

        [Display(Name = "İşlem Gördü Mü")]
        [Required]
        public bool IslemGorduMu { get; set; }

        [Display(Name = "Durum")]
        [DataType(DataType.Text)]
        public string Durum { get; set; }


        public static List<CagriTamamlamaBilgileri> cagriTamamlamaList = new List<CagriTamamlamaBilgileri>();
    }
}