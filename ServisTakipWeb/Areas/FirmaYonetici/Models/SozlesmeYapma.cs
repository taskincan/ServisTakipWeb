using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.FirmaYonetici.Models
{
    public class SozlesmeYapma : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int SozlesmeID { get; set; }
        
        public int FyID { get; set; }
        
        public int MID { get; set; }

        public static List<SozlesmeYapma> sozlesmeYapmaList = new List<SozlesmeYapma>();
    }
}