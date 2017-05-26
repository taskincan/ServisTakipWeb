using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Musteri.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.MusteriYonetici.Controllers
{
    public class ProfileController : BaseController
    {
        //
        // GET: /MusteriYonetici/Profile/

        public ActionResult Index()
        {
            Connection.sayfaAdi = "Müşteri Yönetici Profil";

            var _musteriYonetici = new MusteriYoneticiBilgileri();

            var musteriYonetici = dbMusteri.MusteriYonetici.SingleOrDefault(x => x.MyID == Connection.ID);

            _musteriYonetici.MyID = musteriYonetici.MyID;
            _musteriYonetici.UserName = musteriYonetici.UserName;
            _musteriYonetici.Password = musteriYonetici.Password;
            _musteriYonetici.Ad = musteriYonetici.Ad;
            _musteriYonetici.Soyad = musteriYonetici.Soyad;
            _musteriYonetici.Gsm = musteriYonetici.Gsm;
            _musteriYonetici.Email = musteriYonetici.Email;
            _musteriYonetici.MusteriID = musteriYonetici.MusteriID;
            _musteriYonetici.Password = musteriYonetici.Password;
            _musteriYonetici.CreateDate = musteriYonetici.CreateDate;

            if (_musteriYonetici == null)
            {
                return RedirectToAction("Index");
            }

            return View(_musteriYonetici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MusteriYoneticiBilgileri _musteriYonetici)
        {
            if (ModelState.IsValid)
            {
                var _user = dbMusteri.MusteriYonetici.SingleOrDefault(x => x.MyID == _musteriYonetici.MyID);

                _user.MyID = _musteriYonetici.MyID;
                _user.UserName = _musteriYonetici.UserName;
                _user.Password = _musteriYonetici.Password;
                _user.Ad = _musteriYonetici.Ad;
                _user.Soyad = _musteriYonetici.Soyad;
                _user.Gsm = _musteriYonetici.Gsm;
                _user.Email = _musteriYonetici.Email;
                _user.MusteriID = _musteriYonetici.MusteriID;
                _user.CreateDate = DateTime.Now;

                dbMusteri.Entry(_user).State = EntityState.Modified;
                dbMusteri.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(_musteriYonetici);
        } 

    }
}
