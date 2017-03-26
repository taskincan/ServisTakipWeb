using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Firma.Context;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.Firma.Controllers
{
    public class YoneticiBilgileriController : BaseController
    {      
      
        //private ServisTakipFirmaEntities db = new ServisTakipFirmaEntities();

        //private ServisTakipFirmaEntities db2 = new ServisTakipFirmaEntities();
        //
        // GET: /Firma/YoneticiBilgileri/

        public ActionResult Index()
        {
            //ListTemizle();
            //ListYarat();

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}
