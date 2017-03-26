using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.Firma.Controllers
{
    public class AnaSayfaController : BaseController
    {
        //
        // GET: /Firma/AnaSayfa/

        public ActionResult Index()
        {
            return View();
        }

    }
}
