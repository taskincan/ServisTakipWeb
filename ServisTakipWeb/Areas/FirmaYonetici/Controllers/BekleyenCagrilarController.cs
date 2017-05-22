using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class BekleyenCagrilarController : BaseController
    {
        //
        // GET: /FirmaYonetici/BekleyenCagrilar/

        public ActionResult Index()
        {
            return View();
        }

    }
}
