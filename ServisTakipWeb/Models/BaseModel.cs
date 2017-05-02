using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServisTakipWeb.Models
{
    public class BaseModel
    {
        [Display(Name = "Kayıt Tarihi")]
        public DateTime CreateDate { get; set; }
    }
}