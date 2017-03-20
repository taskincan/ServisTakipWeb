using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisTakipWeb.Areas.Admin.Controllers
{
    public class FirmaBilgileriController : Controller
    {
        //
        // GET: /Admin/FirmaBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            return View(Models.FirmaBilgileri.firmaList.ToList());
        }

        private void ListYarat()
        {
            int temp, passLength=0;

            var context = new Context.ServisTakipAdminEntities();

            for (temp = 0; temp < context.Firma.Count(); temp++)
            {
                var _firmalist = new Models.FirmaBilgileri();

                _firmalist.ID = context.Firma.ToList()[temp].ID;
                _firmalist.FirmaKodu = context.Firma.ToList()[temp].FirmaKodu;
                _firmalist.FirmaAdi = context.Firma.ToList()[temp].FirmaAdi;
                _firmalist.YetkiliKisi = context.Firma.ToList()[temp].YetkiliKisi;
                _firmalist.Gsm = context.Firma.ToList()[temp].Gsm;
                _firmalist.FirmaTel = context.Firma.ToList()[temp].FirmaTel;
                _firmalist.WebSite = context.Firma.ToList()[temp].webSitesi;
                _firmalist.YoneticiUserName = context.Firma.ToList()[temp].YoneticiUserName;

                passLength =  (context.Firma.ToList()[temp].YoneticiPassword).Length;

                for (int temp2 = 0; temp2 < passLength; temp2++)
                {
                    _firmalist.YonetciPassword +="*";
                } 
                _firmalist.Adres = context.Firma.ToList()[temp].Adres;
                _firmalist.Email = context.Firma.ToList()[temp].Email;
                _firmalist.AdminID = context.Firma.ToList()[temp].AdminID;
                _firmalist.CreateDate = Convert.ToDateTime(context.Firma.ToList()[temp].CreateDate);

                Models.FirmaBilgileri.firmaList.Add(_firmalist);
            }
        } 

        private void ListTemizle()
        {
            Models.FirmaBilgileri.firmaList.Clear();
        }

    }
}
