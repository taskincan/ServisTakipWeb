using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Musteri.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.Musteri.Controllers
{
    public class YoneticiBilgileriController : BaseController
    {
        //
        // GET: /Musteri/YoneticiBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            Connection.sayfaAdi = "Yönetici Bilgileri";

            return View(MusteriYoneticiBilgileri.musteriYoneticiList);
        }

        public ActionResult Edit(int id = 0)
        {
            var _musteriYonetici = MusteriYoneticiBilgileri.musteriYoneticiList.SingleOrDefault(x=>x.MyID == id);
  
            if (_musteriYonetici == null)
            {
                return RedirectToAction("Index");
            }

            return View(_musteriYonetici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MusteriYoneticiBilgileri _musteriYonetici)
        {
            if (ModelState.IsValid)
            {
                var _user = dbMusteri.MusteriYonetici.SingleOrDefault(x=>x.MyID == _musteriYonetici.MyID);

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

        public ActionResult Create()
        {
            var _musteriYonetici = new MusteriYoneticiBilgileri();

            _musteriYonetici.MusteriID = Connection.ID;

            return View(_musteriYonetici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusteriYoneticiBilgileri _musteriYoneticiBilgileri)
        {  
            ViewBag.Message = "";

            var user = dbMusteri.MusteriYonetici.SingleOrDefault(c => c.UserName == _musteriYoneticiBilgileri.UserName);

            if (user == null) //database de ayni yonetici UserName yok. Kayıt yapılabilir.
            {
                var _musteriYonetici = new Context.MusteriYonetici();

                if (ModelState.IsValid)
                { 
                    _musteriYonetici.UserName = _musteriYoneticiBilgileri.UserName;
                    _musteriYonetici.Password = _musteriYoneticiBilgileri.Password;
                    _musteriYonetici.Ad = _musteriYoneticiBilgileri.Ad;
                    _musteriYonetici.Soyad = _musteriYoneticiBilgileri.Soyad;
                    _musteriYonetici.Gsm = _musteriYoneticiBilgileri.Gsm;
                    _musteriYonetici.Email = _musteriYoneticiBilgileri.Email;
                    _musteriYonetici.MusteriID = _musteriYoneticiBilgileri.MusteriID;
                    _musteriYonetici.CreateDate = DateTime.Now;

                    dbMusteri.MusteriYonetici.Add(_musteriYonetici);
                    dbMusteri.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(_musteriYoneticiBilgileri);
            }
            else //aynı kullanici isminden varsa kayit yapmadan sayfaya ViewBag.Message yolluyor.
            { 
                ViewBag.Message = "Farklı bir Kullanıcı Adı giriniz.";

                return View(_musteriYoneticiBilgileri);
            }
        }


        private void ListYarat()
        {
            MusteriYoneticiBilgileri.musteriYoneticiList.Clear();

            int temp, passLength = 0, count = 0;
            count = dbMusteri.MusteriYonetici.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (Connection.ID == dbMusteri.MusteriYonetici.ToList()[temp].MusteriID)
                {
                    var _musteriYonetici = new MusteriYoneticiBilgileri();
                    var musteriYonetici = dbMusteri.MusteriYonetici.ToList()[temp];

                    _musteriYonetici.MyID = musteriYonetici.MyID;
                    _musteriYonetici.UserName = musteriYonetici.UserName;
                    _musteriYonetici.Password = musteriYonetici.Password;
                    _musteriYonetici.Ad = musteriYonetici.Ad;
                    _musteriYonetici.Soyad = musteriYonetici.Soyad;
                    _musteriYonetici.Gsm = musteriYonetici.Gsm;
                    _musteriYonetici.Email = musteriYonetici.Email;
                    _musteriYonetici.MusteriID = musteriYonetici.MusteriID;

                    passLength = (musteriYonetici.Password).Length;

                    for (int temp2 = 0; temp2 < passLength; temp2++)
                    {
                        _musteriYonetici.Password2 += "*";
                    }

                    _musteriYonetici.CreateDate = musteriYonetici.CreateDate;

                    MusteriYoneticiBilgileri.musteriYoneticiList.Add(_musteriYonetici);
                }
            }
        }

        private void ListTemizle()
        {
            MusteriYoneticiBilgileri.musteriYoneticiList.Clear();
        }

    }
}
