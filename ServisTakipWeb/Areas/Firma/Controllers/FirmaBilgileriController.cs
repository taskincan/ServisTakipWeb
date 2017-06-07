using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Controllers;
using ServisTakipWeb.Areas.Firma.Context;
using ServisTakipWeb.Areas.Firma.Models;

namespace ServisTakipWeb.Areas.Firma.Controllers
{ 
    public class FirmaBilgileriController : BaseController
    {
        //
        // GET: /Firma/FirmaBilgileri/

        public ActionResult Index()
        {
            var _firmaList = new FirmaBilgileri();
            int passLength = 0;

            var user = dbFirma.Firma.SingleOrDefault(x => x.ID == Connection.ID);

            _firmaList.ID = user.ID;
            _firmaList.FirmaKodu = user.FirmaKodu;
            _firmaList.FirmaAdi = user.FirmaAdi;
            _firmaList.YetkiliKisi = user.YetkiliKisi;
            _firmaList.Gsm = user.Gsm;
            _firmaList.FirmaTel = user.FirmaTel;
            _firmaList.WebSite = user.webSitesi;
            _firmaList.UserName = user.UserName;
            _firmaList.Password = user.Password;

            passLength = (user.Password).Length;

            for (int temp2 = 0; temp2 < passLength; temp2++)
            {
                _firmaList.Password2 += "*";
            }
            _firmaList.Adres = user.Adres;
            _firmaList.Email = user.Email;
            _firmaList.AdminID = user.AdminID;
            _firmaList.CreateDate = Convert.ToDateTime(user.CreateDate.ToShortDateString());
                  
            if (_firmaList == null)
            {
                return HttpNotFound();
            }
            return View(_firmaList);
        }  
    }
}
