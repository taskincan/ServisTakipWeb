using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.Admin.Controllers
{
    public class AnaSayfaController : BaseController
    {
        //
        // GET: /Admin/AnaSayfa/

        public ActionResult Index()
        {
            return View();
        }

    }
}
