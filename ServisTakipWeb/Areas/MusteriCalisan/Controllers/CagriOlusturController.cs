using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.MusteriCalisan.Models;

namespace ServisTakipWeb.Areas.MusteriCalisan.Controllers
{
    public class CagriOlusturController : Controller
    {
        //
        // GET: /MusteriCalisan/CagriOlustur/

        public ActionResult Index()
        {
            CagriBilgileri cagri = new CagriBilgileri();

            cagri.CagriNo = 1;
            cagri.Adres = "";
            cagri.MusteriAdi = "";
            cagri.Sozlesme = "";
            cagri.CagriAcilisTarihi = DateTime.Now;

            cagri.IlgiliKisi = "";
            cagri.Telefon = "";
            cagri.Email = "";
            cagri.CihazTipi = "";
            cagri.Marka = "";
            cagri.Model = "";
            cagri.SeriNo = "";
            cagri.BarkodNo = "";
            cagri.Aciklama ="";
            cagri.CagriDetayi = "";
            cagri.SarfMalzemeTalebi = "";
            cagri.CreateDate = DateTime.Now;
            cagri.CreateUserID = Connection.parentID;

            return View(cagri);
        }

    }
}
