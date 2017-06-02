using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.MusteriYonetici.Models
{
    public class AnketSorulari : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int FirmaID { get; set; }

        public int MyID { get; set; }

        public int CagriNo { get; set; }

        [Display(Name = "Anketi Yapan")]
        public string MyAdiSoyadi { get; set; }

        [Display(Name = "1 (Çok Kötü)  |  2 (Kötü)  |  3 (Orta)  |  4 (İyi)  |  5 (Çok İyi)")]
        public string Aciklama { get; set; }

        [Display(Name = "Anlaşmalı olduğunuz firmadan ne kadar memnunsunuz ?")]
        [Required]
        public string Soru1 { get; set; }

        [Display(Name = "Servisi tamamlayan firma çalışanından ne kadar memnun kaldınız ?")]
        [Required]
        public string Soru2 { get; set; }

        [Display(Name = "Servis çağrınıza geri dönüş süresinden ne kadar memnun kaldınız ?")]
        [Required]
        public string Soru3 { get; set; }

        [Display(Name = "Servis çağrınızın çözüm yönteminden ne kadar memnun kaldınız ?")]
        [Required]
        public string Soru4 { get; set; }

        [Display(Name = "Servis Yönetim ve Takip Sisteminden ne kadar memnunsunuz ?")]
        [Required]
        public string Soru5 { get; set; }

        [Display(Name = "Belirtmek istediğiniz görüşleriniz.")]
        public string MusteriGorusu { get; set; } 

        public double AnketOrtalamaPuani { get; set; }

        public static List<AnketSorulari> anketList = new List<AnketSorulari>();
    }
}