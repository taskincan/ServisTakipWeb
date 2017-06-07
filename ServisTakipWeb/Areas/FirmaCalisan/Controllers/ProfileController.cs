using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaCalisan.Controllers
{
    public class ProfileController : BaseController
    {
        //
        // GET: /FirmaCalisan/Profile/

        public ActionResult Index()
        {
            Connection.sayfaAdi = "Firma Çalışan Profil";

            var _firmaCalisan = new CalisanBilgileri();

            var firmaCalisan = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(x => x.FcID == Connection.ID);

            _firmaCalisan.FcID = firmaCalisan.FcID;
            _firmaCalisan.UserName = firmaCalisan.UserName;
            _firmaCalisan.Password = firmaCalisan.Password;
            _firmaCalisan.Ad = firmaCalisan.Ad;
            _firmaCalisan.Soyad = firmaCalisan.Soyad;
            _firmaCalisan.Gsm = firmaCalisan.Gsm;
            _firmaCalisan.Email = firmaCalisan.Email;
            _firmaCalisan.FirmaID = firmaCalisan.FirmaID;
            _firmaCalisan.CreateUserID = firmaCalisan.CreateUserID;
            _firmaCalisan.Password = firmaCalisan.Password;
            _firmaCalisan.CreateDate = firmaCalisan.CreateDate;

            if (_firmaCalisan == null)
            {
                return RedirectToAction("Index");
            }

            return View(_firmaCalisan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CalisanBilgileri _firmaCalisan)
        {
            if (ModelState.IsValid)
            {
                var _user = new FirmaYonetici.Context.FirmaCalisani();

                _user.FcID = _firmaCalisan.FcID;
                _user.UserName = _firmaCalisan.UserName;
                _user.Password = _firmaCalisan.Password;
                _user.Ad = _firmaCalisan.Ad;
                _user.Soyad = _firmaCalisan.Soyad;
                _user.Gsm = _firmaCalisan.Gsm;
                _user.CreateUserID = _firmaCalisan.CreateUserID;
                _user.Email = _firmaCalisan.Email;
                _user.FirmaID = _firmaCalisan.FirmaID;
                _user.CreateDate = DateTime.Now;

                dbFirmaYonetici.Entry(_user).State = EntityState.Modified;
                dbFirmaYonetici.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index", "AnaSayfa");
            }
            return View(_firmaCalisan);
        } 

    }
}
