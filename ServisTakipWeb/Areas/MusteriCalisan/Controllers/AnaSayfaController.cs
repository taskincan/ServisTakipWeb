using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.MusteriCalisan.Controllers
{
    public class AnaSayfaController : BaseController
    {
        //
        // GET: /MusteriCalisan/AnaSayfa/

        public ActionResult Index()
        {
            return View();
        }
         
    }
}
