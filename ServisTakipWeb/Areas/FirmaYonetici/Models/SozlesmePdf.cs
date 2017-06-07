using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServisTakipWeb.Models;

namespace ServisTakipWeb.Areas.FirmaYonetici.Models
{
    public class SozlesmePdf : BaseModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public HttpPostedFileBase files { get; set; }

        public int Id { get; set; }
        [Display(Name = "Uploaded File")]
        public String FileName { get; set; }
        public byte[] FileContent { get; set; }  
    }
}