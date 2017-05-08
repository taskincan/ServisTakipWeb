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

        public ActionResult Index(int id = 0)
        {
            id = Connection.ID;

            Connection.sayfaAdi = "Firma Yönetici Profil";

            var _firmaYonetici = new YoneticiBilgileri();

            ListTemizle();
            ListYarat(_firmaYonetici,id);


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

                return RedirectToAction("Index");
            }
            return View(_firmaYoneticiBilgileri);
        }


        private void ListYarat(YoneticiBilgileri _firmaYoneticiList, int _id)
        {
            YoneticiBilgileri.firmaYoneticiList.Clear();

            int temp, passLength = 0, count = 0;

            count = dbFirmaYonetici.FirmaYonetici.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (_id == dbFirmaYonetici.FirmaYonetici.ToList()[temp].FyID)
                { 
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
                        _firmaYoneticiList.YoneteciPassword2 += "*";
                    }

                    _firmaYoneticiList.CreateDate = dbFirmaYonetici.FirmaYonetici.ToList()[temp].CreateDate;

                    YoneticiBilgileri.firmaYoneticiList.Add(_firmaYoneticiList);
                    break;
                }
            }
        }

        private void ListTemizle()
        {
            YoneticiBilgileri.firmaYoneticiList.Clear();
        }

    }
}
