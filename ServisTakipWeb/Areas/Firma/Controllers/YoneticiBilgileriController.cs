using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Firma.Context;
using ServisTakipWeb.Areas.Firma.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.Firma.Controllers
{
    public class YoneticiBilgileriController : BaseController
    {  
        //
        // GET: /Firma/YoneticiBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            Connection.sayfaAdi = "Yönetici Bilgileri";
            return View(FirmaYönetici.firmaYoneticiList);
        }

        public ActionResult Edit(int id = 0)
        {
            var _firmaYonetici = FirmaYönetici.firmaYoneticiList.SingleOrDefault(x => x.FyID == id);
  
            if (_firmaYonetici == null)
            {
                return RedirectToAction("Index");
            }

            return View(_firmaYonetici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FirmaYönetici _firmaYoneticiBilgileri)
        {
            if (ModelState.IsValid)
            {
                var _user = dbFirma.FirmaYonetici.SingleOrDefault(x => x.FyID == _firmaYoneticiBilgileri.FyID);

                _user.FyID = _firmaYoneticiBilgileri.FyID;
                _user.UserName = _firmaYoneticiBilgileri.UserName;
                _user.Password = _firmaYoneticiBilgileri.Password;
                _user.Ad = _firmaYoneticiBilgileri.Ad;
                _user.Soyad = _firmaYoneticiBilgileri.Soyad;
                _user.Gsm = _firmaYoneticiBilgileri.Gsm;
                _user.Email = _firmaYoneticiBilgileri.Email;
                _user.FirmaID = _firmaYoneticiBilgileri.FirmaID;
                _user.CreateDate = DateTime.Now;

                dbFirma.Entry(_user).State = EntityState.Modified;
                dbFirma.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(_firmaYoneticiBilgileri);
        }


        public ActionResult Create()
        {
            var _firmaYoneticiList = new FirmaYönetici();

            _firmaYoneticiList.FirmaID = Connection.ID;

            return View(_firmaYoneticiList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FirmaYönetici _firmaYoneticiBilgileri)
        {
            bool yoneticiUserNameVarMi = false;

            ViewBag.Message = "";

            var user = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(c => c.UserName == _firmaYoneticiBilgileri.UserName);

            if (user == null) //database de ayni yonetici UserName yok. Kayıt yapılabilir.
            {
                yoneticiUserNameVarMi = false;

                if (ModelState.IsValid)
                {
                    var _firmaYonetici = new Context.FirmaYonetici();

                    _firmaYonetici.UserName = _firmaYoneticiBilgileri.UserName;
                    _firmaYonetici.Password = _firmaYoneticiBilgileri.Password;
                    _firmaYonetici.Ad = _firmaYoneticiBilgileri.Ad;
                    _firmaYonetici.Soyad = _firmaYoneticiBilgileri.Soyad;
                    _firmaYonetici.Gsm = _firmaYoneticiBilgileri.Gsm;
                    _firmaYonetici.Email = _firmaYoneticiBilgileri.Email;
                    _firmaYonetici.FirmaID = _firmaYoneticiBilgileri.FirmaID;
                    _firmaYonetici.CreateDate = DateTime.Now;

                    dbFirma.FirmaYonetici.Add(_firmaYonetici);
                    dbFirma.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(_firmaYoneticiBilgileri);
            }
            else //aynı kullanici isminden varsa kayit yapmadan sayfaya ViewBag.Message yolluyor.
            {
                yoneticiUserNameVarMi = true; //database de ayni yonetici UserName var.
                ViewBag.Message = "Farklı bir Kullanıcı Adı giriniz.";

                return View(_firmaYoneticiBilgileri);
            }
        }

        private void ListYarat()
        {
            FirmaYönetici.firmaYoneticiList.Clear();

            int temp, passLength = 0, count = 0;
            count = dbFirma.FirmaYonetici.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (Connection.ID == dbFirma.FirmaYonetici.ToList()[temp].FirmaID)  
                {
                    var _firmaYoneticiList = new FirmaYönetici();

                    _firmaYoneticiList.FyID = dbFirma.FirmaYonetici.ToList()[temp].FyID;
                    _firmaYoneticiList.UserName = dbFirma.FirmaYonetici.ToList()[temp].UserName;
                    _firmaYoneticiList.Password = dbFirma.FirmaYonetici.ToList()[temp].Password;
                    _firmaYoneticiList.Ad = dbFirma.FirmaYonetici.ToList()[temp].Ad;
                    _firmaYoneticiList.Soyad = dbFirma.FirmaYonetici.ToList()[temp].Soyad;
                    _firmaYoneticiList.Gsm = dbFirma.FirmaYonetici.ToList()[temp].Gsm;
                    _firmaYoneticiList.Email = dbFirma.FirmaYonetici.ToList()[temp].Email;
                    _firmaYoneticiList.FirmaID = dbFirma.FirmaYonetici.ToList()[temp].FirmaID;

                    passLength = (dbFirma.FirmaYonetici.ToList()[temp].Password).Length;

                    for (int temp2 = 0; temp2 < passLength; temp2++)
                    {
                        _firmaYoneticiList.Password2 += "*";
                    }

                    _firmaYoneticiList.CreateDate = dbFirma.FirmaYonetici.ToList()[temp].CreateDate;

                    FirmaYönetici.firmaYoneticiList.Add(_firmaYoneticiList);
                }
            }
        }

        private void ListTemizle()
        {
            FirmaYönetici.firmaYoneticiList.Clear();
        }


    }
}
