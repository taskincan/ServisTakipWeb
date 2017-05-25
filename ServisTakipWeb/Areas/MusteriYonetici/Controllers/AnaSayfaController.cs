using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.MusteriYonetici.Controllers
{
    public class AnaSayfaController : BaseController
    {
        //
        // GET: /MusteriYonetici/AnaSayfa/

        public ActionResult Index()
        {
            return View();
        }

    }
}
