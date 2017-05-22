using ServisTakipWeb.Areas.Firma.Models;
using ServisTakipWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class YoneticiBilgileriController : BaseController
    {
        //
        // GET: /FirmaYonetici/YoneticiBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            Connection.sayfaAdi = "Yönetici Bilgileri";

            return View(FirmaYönetici.firmaYoneticiList);
        }

        public ActionResult Edit(int id = 0)
        {
            var _firmaYonetici = new FirmaYönetici();

            foreach (var item in FirmaYönetici.firmaYoneticiList)
            {
                if (item.FyID == id)
                {
                    _firmaYonetici.FyID = item.FyID;
                    _firmaYonetici.UserName = item.UserName;
                    _firmaYonetici.Password = item.Password;
                    _firmaYonetici.Ad = item.Ad;
                    _firmaYonetici.Soyad = item.Soyad;
                    _firmaYonetici.Gsm = item.Gsm;
                    _firmaYonetici.Email = item.Email;
                    _firmaYonetici.FirmaID = item.FirmaID;
                }
            }

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
                var _user = new Context.FirmaYonetici();

                _user.FyID = _firmaYoneticiBilgileri.FyID;
                _user.UserName = _firmaYoneticiBilgileri.UserName;
                _user.Password = _firmaYoneticiBilgileri.Password;
                _user.Ad = _firmaYoneticiBilgileri.Ad;
                _user.Soyad = _firmaYoneticiBilgileri.Soyad;
                _user.Gsm = _firmaYoneticiBilgileri.Gsm;
                _user.Email = _firmaYoneticiBilgileri.Email;
                _user.FirmaID = _firmaYoneticiBilgileri.FirmaID;
                _user.CreateDate = DateTime.Now;

                dbFirmaYonetici.Entry(_user).State = EntityState.Modified;
                dbFirmaYonetici.SaveChanges();
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

                    dbFirmaYonetici.FirmaYonetici.Add(_firmaYonetici);
                    dbFirmaYonetici.SaveChanges();

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
                if (Connection.parentID == dbFirmaYonetici.FirmaYonetici.ToList()[temp].FirmaID)
                {
                    var _firmaYoneticiList = new FirmaYönetici();

                    _firmaYoneticiList.FyID = dbFirmaYonetici.FirmaYonetici.ToList()[temp].FyID;
                    _firmaYoneticiList.UserName = dbFirmaYonetici.FirmaYonetici.ToList()[temp].UserName;
                    _firmaYoneticiList.Password = dbFirmaYonetici.FirmaYonetici.ToList()[temp].Password;
                    _firmaYoneticiList.Ad = dbFirmaYonetici.FirmaYonetici.ToList()[temp].Ad;
                    _firmaYoneticiList.Soyad = dbFirmaYonetici.FirmaYonetici.ToList()[temp].Soyad;
                    _firmaYoneticiList.Gsm = dbFirmaYonetici.FirmaYonetici.ToList()[temp].Gsm;
                    _firmaYoneticiList.Email = dbFirmaYonetici.FirmaYonetici.ToList()[temp].Email;
                    _firmaYoneticiList.FirmaID = dbFirmaYonetici.FirmaYonetici.ToList()[temp].FirmaID;

                    passLength = (dbFirmaYonetici.FirmaYonetici.ToList()[temp].Password).Length;

                    for (int temp2 = 0; temp2 < passLength; temp2++)
                    {
                        _firmaYoneticiList.Password2 += "*";
                    }

                    _firmaYoneticiList.CreateDate = dbFirmaYonetici.FirmaYonetici.ToList()[temp].CreateDate;

                    FirmaYönetici.firmaYoneticiList.Add(_firmaYoneticiList);
                }
            }
            FirmaYönetici.firmaYoneticiList = FirmaYönetici.firmaYoneticiList.OrderBy(x=>x.CreateDate).ToList();
        }

        private void ListTemizle()
        {
            FirmaYönetici.firmaYoneticiList.Clear();
        }

    }
}
