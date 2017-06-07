using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.FirmaYonetici.Models
{
    public class SozlesmeBilgileri : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SozlesmeID { get; set; } 

        public int MusteriID { get; set; }
 
        public string MusteriKodu { get; set; }

        [Display(Name = "Sözleşme Adı")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string SozlesmeAdi { get; set; }

        [Display(Name = "Anlaşma Ücreti (TL)")]
        [Required] 
        public double AnlasmaUcreti { get; set; }

        [Display(Name = "Sla Süresi (Saat)")] 
        [Required] 
        public int SlaSuresi { get; set; }

        [Display(Name = "Parça Dahil Mi")]
        [Required]
        public bool ParcaDahilMi { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarih { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        public DateTime BitisTarih { get; set; }
         
        
        
        [DataType(DataType.Upload)]
        [Display(Name = "Sözleşme Seç")]
        public HttpPostedFileBase files { get; set; }

        public int Idpdf { get; set; }

        [Display(Name = "Sözleşme")]
        public String FileName { get; set; }

        public byte[] FileContent { get; set; }


        public static List<SozlesmeBilgileri> pdfList = new List<SozlesmeBilgileri>();


        public static List<SozlesmeBilgileri> sozlesmeList = new List<SozlesmeBilgileri>();
    }
}