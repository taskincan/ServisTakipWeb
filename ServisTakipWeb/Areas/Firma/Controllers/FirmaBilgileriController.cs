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

        //TODO: Edit olmayacak Post Index olacak. Profile de ki gibi.
        [HttpPost]
        public ActionResult Edit(FirmaBilgileri _firmaBilgileri)
        {
            int temp;
            bool firmaKoduVarMi = false;
            ViewBag.Message = "";

            for (temp = 0; temp < dbFirma.Firma.Count(); temp++)
            {
                if (_firmaBilgileri.FirmaKodu == dbFirma.Firma.ToList()[temp].FirmaKodu)
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

                    dbFirma.Entry(_firma).State = EntityState.Modified;
                    dbFirma.SaveChanges();
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
            count = dbFirma.Firma.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (_firmaBilgileri.FirmaKodu == dbFirma.Firma.ToList()[temp].FirmaKodu)
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

                    dbFirma.Firma.Add(_firma);
                    dbFirma.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(_firmaBilgileri);
            }
        }

        private void ListYarat(int _Id)
        {
            FirmaBilgileri.firmaList.Clear();

            int temp, passLength = 0, count = 0;


            var user = dbFirma.Firma.SingleOrDefault(x => x.ID == _Id);

            count = dbFirma.Firma.Count();
             
            for (temp = 0; temp < count; temp++)
            {
                if (dbFirma.Firma.ToList()[temp].ID == _Id)
                {
                    var _firmaList = new FirmaBilgileri();

                    _firmaList.ID = dbFirma.Firma.ToList()[temp].ID;
                    _firmaList.FirmaKodu = dbFirma.Firma.ToList()[temp].FirmaKodu;
                    _firmaList.FirmaAdi = dbFirma.Firma.ToList()[temp].FirmaAdi;
                    _firmaList.YetkiliKisi = dbFirma.Firma.ToList()[temp].YetkiliKisi;
                    _firmaList.Gsm = dbFirma.Firma.ToList()[temp].Gsm;
                    _firmaList.FirmaTel = dbFirma.Firma.ToList()[temp].FirmaTel;
                    _firmaList.WebSite = dbFirma.Firma.ToList()[temp].webSitesi;
                    _firmaList.UserName = dbFirma.Firma.ToList()[temp].UserName;
                    _firmaList.Password = dbFirma.Firma.ToList()[temp].Password;

                    passLength = (dbFirma.Firma.ToList()[temp].Password).Length;

                    for (int temp2 = 0; temp2 < passLength; temp2++)
                    {
                        _firmaList.Password2 += "*";
                    }

                    _firmaList.Adres = dbFirma.Firma.ToList()[temp].Adres;
                    _firmaList.Email = dbFirma.Firma.ToList()[temp].Email;
                    _firmaList.AdminID = dbFirma.Firma.ToList()[temp].AdminID;
                    _firmaList.CreateDate = Convert.ToDateTime(dbFirma.Firma.ToList()[temp].CreateDate.ToShortDateString());

                    FirmaBilgileri.firmaList.Add(_firmaList);
                    break;
                }
            }
        }

        private void ListTemizle()
        {
            FirmaBilgileri.firmaList.Clear();
        }

    }
}
