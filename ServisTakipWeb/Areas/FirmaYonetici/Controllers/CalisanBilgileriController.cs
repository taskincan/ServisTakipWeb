using ServisTakipWeb.Areas.FirmaYonetici.Context;
using ServisTakipWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class CalisanBilgileriController : BaseController
    { 
        //
        // GET: /FirmaYonetici/CalisanBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            return View();
        }

        private void ListYarat()
        {
            throw new NotImplementedException();
        }

        private void ListTemizle()
        {
            throw new NotImplementedException();
        }

    }
}
