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
        private ServisTakipFirmaDbEntities _db = null;

        public ServisTakipFirmaDbEntities db
        {
            get
            {
                if (_db == null)
                {
                    _db = new ServisTakipFirmaDbEntities();
                    _db.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _db;
            }
        }

        //
        // GET: /Firma/YoneticiBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

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

                db.Entry(_user).State = EntityState.Modified;
                db.SaveChanges();
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
            int temp, count = 0;
            bool yoneticiUserNameVarMi = false;

            ViewBag.Message = "";
            count = db.FirmaYonetici.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (_firmaYoneticiBilgileri.UserName == db.FirmaYonetici.ToList()[temp].UserName)
                {
                    yoneticiUserNameVarMi = true; //database de ayni yonetici UserName var.
                }
            }

            if (yoneticiUserNameVarMi == true) //aynı kullanici isminden varsa kayit yapmadan sayfaya ViewBag.Message yolluyor.
            {
                ViewBag.Message = "Farklı bir Kullanıcı Adı giriniz.";
                return View(_firmaYoneticiBilgileri);
            }
            else
            {
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

                    db.FirmaYonetici.Add(_firmaYonetici);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(_firmaYoneticiBilgileri);
            }
        }

        private void ListYarat()
        {
            FirmaYönetici.firmaYoneticiList.Clear();

            int temp, passLength = 0, count = 0;
            count = db.FirmaYonetici.Count();

            for (temp = 0; temp < count; temp++)
            {
                if ( Connection.ID == db.FirmaYonetici.ToList()[temp].FirmaID)  
                {
                    var _firmaYoneticiList = new FirmaYönetici();

                    _firmaYoneticiList.FyID = db.FirmaYonetici.ToList()[temp].FyID;
                    _firmaYoneticiList.UserName = db.FirmaYonetici.ToList()[temp].UserName;
                    _firmaYoneticiList.Password = db.FirmaYonetici.ToList()[temp].Password;
                    _firmaYoneticiList.Ad = db.FirmaYonetici.ToList()[temp].Ad;
                    _firmaYoneticiList.Soyad = db.FirmaYonetici.ToList()[temp].Soyad;
                    _firmaYoneticiList.Gsm = db.FirmaYonetici.ToList()[temp].Gsm;
                    _firmaYoneticiList.Email = db.FirmaYonetici.ToList()[temp].Email;
                    _firmaYoneticiList.FirmaID = db.FirmaYonetici.ToList()[temp].FirmaID;

                    passLength = (db.FirmaYonetici.ToList()[temp].Password).Length;

                    for (int temp2 = 0; temp2 < passLength; temp2++)
                    {
                        _firmaYoneticiList.YoneteciPassword2 += "*";
                    }

                    _firmaYoneticiList.CreateDate = db.FirmaYonetici.ToList()[temp].CreateDate.ToShortDateString();

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
