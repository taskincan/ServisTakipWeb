using ServisTakipWeb.Areas.FirmaYonetici.Context;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class ProfileController : BaseController
    {
        //
        // GET: /FirmaYonetici/Profile/

        public ActionResult Index()
        { 
            Connection.sayfaAdi = "Firma Yönetici Profil";

            var _firmaYonetici = new YoneticiBilgileri();
              
            var firmaYonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(x => x.FyID == Connection.ID);

            _firmaYonetici.FyID = firmaYonetici.FyID;
            _firmaYonetici.UserName = firmaYonetici.UserName;
            _firmaYonetici.Password = firmaYonetici.Password;
            _firmaYonetici.Ad = firmaYonetici.Ad;
            _firmaYonetici.Soyad = firmaYonetici.Soyad;
            _firmaYonetici.Gsm = firmaYonetici.Gsm;
            _firmaYonetici.Email = firmaYonetici.Email;
            _firmaYonetici.FirmaID = firmaYonetici.FirmaID;
            _firmaYonetici.Password = firmaYonetici.Password;
            _firmaYonetici.CreateDate = firmaYonetici.CreateDate;

            if (_firmaYonetici == null)
            {
                return RedirectToAction("Index");
            }

            return View(_firmaYonetici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(YoneticiBilgileri _firmaYoneticiBilgileri)
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

                return RedirectToAction("Index","AnaSayfa");
            }
            return View(_firmaYoneticiBilgileri);
        } 
    }
}
