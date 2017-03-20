using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Admin.Models;
using ServisTakipWeb.Areas.Admin.Context;

namespace ServisTakipWeb.Areas.Admin.Controllers
{
    public class FirmaBilgileriController : Controller
    {
        private ServisTakipAdminEntities db = new ServisTakipAdminEntities();
        private ServisTakipAdminEntities db2 = new ServisTakipAdminEntities();

        //
        // GET: /Admin/FirmaBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            return View(Models.FirmaBilgileri.firmaList.ToList());
        }

        public ActionResult Edit(int id = 0)
        {
            var _firmaList = new FirmaBilgileri();

            foreach (var item in FirmaBilgileri.firmaList)
            {
                if (item.ID == id)
                {
                    _firmaList.ID = item.ID;
                    _firmaList.FirmaKodu = item.FirmaKodu;
                    _firmaList.FirmaAdi = item.FirmaAdi;
                    _firmaList.YetkiliKisi = item.YetkiliKisi;
                    _firmaList.Gsm = item.Gsm;
                    _firmaList.FirmaTel = item.FirmaTel;
                    _firmaList.WebSite = item.WebSite;
                    _firmaList.YoneticiUserName = item.YoneticiUserName;
                    _firmaList.YoneticiPassword = item.YoneticiPassword;
                    _firmaList.Adres = item.Adres;
                    _firmaList.Email = item.Email;
                    _firmaList.AdminID = item.AdminID;  
                    _firmaList.CreateDate = Convert.ToDateTime(item.CreateDate.ToShortDateString());
                }
            }

            if (_firmaList == null)
            {
                return HttpNotFound();
            }
            return View(_firmaList);
        }

        [HttpPost]
        public ActionResult Edit (FirmaBilgileri _firmaBilgileri)
        {
            int temp;
            bool firmaKoduVarMi = false;
            ViewBag.Message = "";

            for (temp = 0; temp < db.Firma.Count(); temp++)
            {
                if (_firmaBilgileri.FirmaKodu == db.Firma.ToList()[temp].FirmaKodu)
                {
                    firmaKoduVarMi = true; //database de ayni firma kodu var.
                }
            }
            if (firmaKoduVarMi == true) //aynı kullanici isminden varsa kayit yapmadan sayfaya ViewBag.Message yolluyor.
            {
                ViewBag.Message = "Farklı bir Firma Kodu deneyiniz.";
                return View(_firmaBilgileri);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var _firma = new Context.Firma();
                    db = new ServisTakipAdminEntities();

                    _firma.ID = _firmaBilgileri.ID;
                    _firma.FirmaKodu = _firmaBilgileri.FirmaKodu;
                    _firma.FirmaAdi = _firmaBilgileri.FirmaAdi;
                    _firma.YetkiliKisi = _firmaBilgileri.YetkiliKisi;
                    _firma.Gsm = _firmaBilgileri.Gsm;
                    _firma.FirmaTel = _firmaBilgileri.FirmaTel;
                    _firma.webSitesi = _firmaBilgileri.WebSite;
                    _firma.YoneticiUserName = _firmaBilgileri.YoneticiUserName;
                    _firma.YoneticiPassword = _firmaBilgileri.YoneticiPassword;
                    _firma.Adres = _firmaBilgileri.Adres;
                    _firma.Email = _firmaBilgileri.Email;

                    _firma.AdminID = _firmaBilgileri.AdminID;

                    _firma.CreateDate = DateTime.Now;

                    //db.Entry(_user).State = EntityState.Modified;

                    db.Entry(_firma).State = EntityState.Modified;
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }

             return View(_firmaBilgileri);
        }

        public ActionResult Create( )
        {
            var _firmaList = new FirmaBilgileri();

            _firmaList.AdminID = Connection.ID;
            
            return View(_firmaList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (FirmaBilgileri _firmaBilgileri)
        {
            int temp;
            bool firmaKoduVarMi = false;
            ViewBag.Message = "";

            for (temp = 0; temp < db.Firma.Count(); temp++)
            {
                if (_firmaBilgileri.FirmaKodu == db.Firma.ToList()[temp].FirmaKodu)
                {
                    firmaKoduVarMi = true; //database de ayni firma kodu var.
                }
            }
            if (firmaKoduVarMi == true) //aynı kullanici isminden varsa kayit yapmadan sayfaya ViewBag.Message yolluyor.
            {
                ViewBag.Message = "Farklı bir Firma Kodu deneyiniz.";
                return View(_firmaBilgileri);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var _firma = new Context.Firma(); 

                    _firma.ID = _firmaBilgileri.ID;
                    _firma.FirmaKodu = _firmaBilgileri.FirmaKodu;
                    _firma.FirmaAdi = _firmaBilgileri.FirmaAdi;
                    _firma.YetkiliKisi = _firmaBilgileri.YetkiliKisi;
                    _firma.Gsm = _firmaBilgileri.Gsm;
                    _firma.FirmaTel = _firmaBilgileri.FirmaTel;
                    _firma.webSitesi = _firmaBilgileri.WebSite;
                    _firma.YoneticiUserName = _firmaBilgileri.YoneticiUserName;
                    _firma.YoneticiPassword = _firmaBilgileri.YoneticiPassword;
                    _firma.Adres = _firmaBilgileri.Adres;
                    _firma.Email = _firmaBilgileri.Email;
                    _firma.AdminID = _firmaBilgileri.AdminID;
                    _firma.CreateDate = DateTime.Now;
                     
                    db.Firma.Add(_firma);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(_firmaBilgileri);
            }
        }

        private void ListYarat()
        {
            FirmaBilgileri.firmaList.Clear();

            int temp, passLength=0;

            var context = new ServisTakipAdminEntities();

            for (temp = 0; temp < context.Firma.Count(); temp++)
            {
                var _firmaList = new FirmaBilgileri();

                _firmaList.ID = context.Firma.ToList()[temp].ID;
                _firmaList.FirmaKodu = context.Firma.ToList()[temp].FirmaKodu;
                _firmaList.FirmaAdi = context.Firma.ToList()[temp].FirmaAdi;
                _firmaList.YetkiliKisi = context.Firma.ToList()[temp].YetkiliKisi;
                _firmaList.Gsm = context.Firma.ToList()[temp].Gsm;
                _firmaList.FirmaTel = context.Firma.ToList()[temp].FirmaTel;
                _firmaList.WebSite = context.Firma.ToList()[temp].webSitesi;
                _firmaList.YoneticiUserName = context.Firma.ToList()[temp].YoneticiUserName;
                _firmaList.YoneticiPassword = context.Firma.ToList()[temp].YoneticiPassword;

                passLength = (context.Firma.ToList()[temp].YoneticiPassword).Length;

                for (int temp2 = 0; temp2 < passLength; temp2++)
                {
                    _firmaList.YoneteciPassword2 += "*";
                }

                _firmaList.Adres = context.Firma.ToList()[temp].Adres;
                _firmaList.Email = context.Firma.ToList()[temp].Email;
                _firmaList.AdminID = context.Firma.ToList()[temp].AdminID;
                _firmaList.CreateDate = Convert.ToDateTime(context.Firma.ToList()[temp].CreateDate.ToShortDateString());

                FirmaBilgileri.firmaList.Add(_firmaList);
            }
        } 

        private void ListTemizle()
        {
            FirmaBilgileri.firmaList.Clear();
        }

    }
}
