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
    public class ProfileController : BaseController
    {
        //
        // GET: /Musteri/Profile/

        public ActionResult Index( )
        {  
            Connection.sayfaAdi = "Müşteri Profil";

            var _musteri = new MusteriBilgileri();

            var musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == Connection.ID);
            var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == musteri.ID);

            if (musteri != null)
            {
                _musteri.Adres = musteri.Adres;
                _musteri.CreateDate = musteri.CreateDate;
                _musteri.Email = musteri.Email;
                _musteri.ID = musteri.ID;
                _musteri.MusteriAdi = musteri.MusteriAdi;
                _musteri.MusteriKodu = musteri.MusteriKodu;
                _musteri.Password = musteri.Password;
                _musteri.Tel1 = musteri.MusteriTel;
                _musteri.Tel2 = musteri.MusteriTel2;
                _musteri.VergiDairesi = musteri.VergiDairesi;
                _musteri.VergiNumarasi = musteri.VergiNumarasi;
                _musteri.YetkiliKisi = musteri.YetkiliKisi;
                _musteri.CreateUserID = sozlesmeYapma.FyID;
            }
            else
                return RedirectToAction("Index");

            return View(_musteri);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MusteriBilgileri _musteriBilgileri)
        {
            if (ModelState.IsValid)
            {
                var musteri = dbMusteri.Musteri.SingleOrDefault(x=>x.MusteriKodu == _musteriBilgileri.MusteriKodu);

                musteri.Adres = _musteriBilgileri.Adres;
                musteri.CreateDate = _musteriBilgileri.CreateDate;
                musteri.Email = _musteriBilgileri.Email;
                musteri.ID = _musteriBilgileri.ID;
                musteri.MusteriAdi = _musteriBilgileri.MusteriAdi;
                musteri.MusteriKodu = _musteriBilgileri.MusteriKodu;
                musteri.Password = _musteriBilgileri.Password;
                musteri.MusteriTel = _musteriBilgileri.Tel1;
                musteri.MusteriTel2 = _musteriBilgileri.Tel2;
                musteri.VergiDairesi = _musteriBilgileri.VergiDairesi;
                musteri.VergiNumarasi = _musteriBilgileri.VergiNumarasi;
                musteri.YetkiliKisi = _musteriBilgileri.YetkiliKisi;
                musteri.CreateDate = DateTime.Now;

                dbMusteri.Entry(musteri).State = EntityState.Modified;
                dbMusteri.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(_musteriBilgileri);
        } 

    }
}
