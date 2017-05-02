using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Admin.Models;
using ServisTakipWeb.Areas.Admin.Context;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.Admin.Controllers
{
    public class FirmaBilgileriController : BaseController
    {
        private ServisTakipAdminDbEntities _db = null;

        public ServisTakipAdminDbEntities db
        {
            get
            {
                if (_db == null)
                {
                    _db = new ServisTakipAdminDbEntities();
                    _db.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _db;
            }
        }

        //
        // GET: /Admin/FirmaBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            return View(FirmaBilgileri.firmaList.ToList());
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
                    _firmaList.UserName = item.UserName;
                    _firmaList.Password = item.Password;
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
        public ActionResult Edit(FirmaBilgileri _firmaBilgileri)
        {
            int temp, count = 0;
            bool firmaKoduVarMi = false;
            ViewBag.Message = "";
 
            count = db.Firma.Count();

            for (temp = 0; temp < count; temp++)
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
                    _firma.UserName = _firmaBilgileri.UserName;
                    _firma.Password = _firmaBilgileri.Password;
                    _firma.Adres = _firmaBilgileri.Adres;
                    _firma.Email = _firmaBilgileri.Email;
                    _firma.AdminID = _firmaBilgileri.AdminID;
                    _firma.CreateDate = DateTime.Now;

                    //TODO : Güncelleme yapmıyor. same primary key hatası alıyor.
                    db.Entry(_firma).State = EntityState.Modified;
                    db.SaveChanges();
                    ModelState.Clear(); 

                    return RedirectToAction("Index");
                }
            }

            return View(_firmaBilgileri);
        }

        public ActionResult Create()
        {
            var _firmaList = new FirmaBilgileri();

            _firmaList.AdminID = Connection.ID;

            return View(_firmaList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FirmaBilgileri _firmaBilgileri)
        {
            int temp, count = 0;
            bool firmaKoduVarMi = false;

            ViewBag.Message = "";
            count = db.Firma.Count();

            for (temp = 0; temp < count; temp++)
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
                    _firma.UserName = _firmaBilgileri.UserName;
                    _firma.Password = _firmaBilgileri.Password;
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

            int temp, passLength = 0, count = 0;
            count = db.Firma.Count();

            for (temp = 0; temp < count; temp++)
            {
                var _firmaList = new FirmaBilgileri();

                _firmaList.ID = db.Firma.ToList()[temp].ID;
                _firmaList.FirmaKodu = db.Firma.ToList()[temp].FirmaKodu;
                _firmaList.FirmaAdi = db.Firma.ToList()[temp].FirmaAdi;
                _firmaList.YetkiliKisi = db.Firma.ToList()[temp].YetkiliKisi;
                _firmaList.Gsm = db.Firma.ToList()[temp].Gsm;
                _firmaList.FirmaTel = db.Firma.ToList()[temp].FirmaTel;
                _firmaList.WebSite = db.Firma.ToList()[temp].webSitesi;
                _firmaList.UserName = db.Firma.ToList()[temp].UserName;
                _firmaList.Password = db.Firma.ToList()[temp].Password;

                passLength = (db.Firma.ToList()[temp].Password).Length;

                for (int temp2 = 0; temp2 < passLength; temp2++)
                {
                    _firmaList.Password2 += "*";
                }

                _firmaList.Adres = db.Firma.ToList()[temp].Adres;
                _firmaList.Email = db.Firma.ToList()[temp].Email;
                _firmaList.AdminID = db.Firma.ToList()[temp].AdminID;
                _firmaList.CreateDate = Convert.ToDateTime(db.Firma.ToList()[temp].CreateDate.ToShortDateString());

                FirmaBilgileri.firmaList.Add(_firmaList);
            }
        }

        private void ListTemizle()
        {
            FirmaBilgileri.firmaList.Clear();
        }

    }
}
