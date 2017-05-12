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

        [Display(Name = "Sözleşme Adı")]
        [DataType(DataType.Text)]
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string SozlesmeAdi { get; set; }

        [Display(Name = "Anlaşma Ücreti")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(1)]
        [MaxLength(7)]
        public string AnlasmaUcreti { get; set; }

        [Display(Name = "Sla Süresi")] 
        [Required] 
        public int SlaSuresi { get; set; }

        [Display(Name = "Parça Dahil Mi")]
        [Required]
        public bool ParcaDahilMi { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarih { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        public DateTime BitisTarih { get; set; }


        public static List<SozlesmeBilgileri> sozlesmeList = new List<SozlesmeBilgileri>();
    }
}