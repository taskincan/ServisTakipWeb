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
    public class CalisanBilgileriController : BaseController
    { 
        //
        // GET: /FirmaYonetici/CalisanBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();
            Connection.sayfaAdi = "Çalışan Bilgileri";

            return View(CalisanBilgileri.firmaCalisanList);
        }

        public ActionResult Edit(int id = 0)
        {
            var _firmaCalisan = new CalisanBilgileri();

            foreach (var item in CalisanBilgileri.firmaCalisanList)
            {
                if (item.FcID == id)
                {
                    _firmaCalisan.FcID = item.FcID;
                    _firmaCalisan.UserName = item.UserName;
                    _firmaCalisan.Password = item.Password;
                    _firmaCalisan.Ad = item.Ad;
                    _firmaCalisan.Soyad = item.Soyad;
                    _firmaCalisan.Gsm = item.Gsm;
                    _firmaCalisan.Email = item.Email;
                    _firmaCalisan.FirmaID = item.FirmaID;
                    _firmaCalisan.CreateUserID = item.CreateUserID;
                }
            }

            if (_firmaCalisan == null)
            {
                return RedirectToAction("Index");
            }

            return View(_firmaCalisan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CalisanBilgileri _firmaCalisanBilgileri)
        {
            if (ModelState.IsValid)
            {
                //var _user = new Context.FirmaCalisani();

                var updatedUser = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(x => x.FcID == _firmaCalisanBilgileri.FcID);

                updatedUser.FcID = _firmaCalisanBilgileri.FcID;
                updatedUser.UserName = _firmaCalisanBilgileri.UserName;
                updatedUser.Password = _firmaCalisanBilgileri.Password;
                updatedUser.Ad = _firmaCalisanBilgileri.Ad;
                updatedUser.Soyad = _firmaCalisanBilgileri.Soyad;
                updatedUser.Gsm = _firmaCalisanBilgileri.Gsm;
                updatedUser.Email = _firmaCalisanBilgileri.Email;
                updatedUser.FirmaID = _firmaCalisanBilgileri.FirmaID;
                updatedUser.CreateUserID = Connection.ID;
                updatedUser.CreateDate = DateTime.Now;

                dbFirmaYonetici.Entry(updatedUser).State = EntityState.Modified;
                dbFirmaYonetici.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(_firmaCalisanBilgileri);
        }

        public ActionResult Create()
        {
            var _firmaCalisanList = new CalisanBilgileri();

            _firmaCalisanList.CreateUserID = Connection.ID;
            _firmaCalisanList.FirmaID = Connection.parentID;

            return View(_firmaCalisanList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalisanBilgileri _firmaCalisanBilgileri)
        {
            int temp, count = 0;
            bool calisanUserNameVarMi = false;

            ViewBag.Message = "";
            
            count = dbFirmaYonetici.FirmaCalisani.Count();
           
            for (temp = 0; temp < count; temp++)
            {
                if ((_firmaCalisanBilgileri.UserName == dbFirmaYonetici.FirmaCalisani.ToList()[temp].UserName) && (_firmaCalisanBilgileri.FirmaID == dbFirmaYonetici.FirmaCalisani.ToList()[temp].FirmaID))
                {
                    calisanUserNameVarMi = true; //database de ayni firmada ayni calisan UserName var.
                    break;
                }
            }
             
            if (calisanUserNameVarMi == true) //aynı kullanici isminden varsa kayit yapmadan sayfaya ViewBag.Message yolluyor.
            {
                ViewBag.Message = "Farklı bir Kullanıcı Adı giriniz.";
                return View(_firmaCalisanBilgileri);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var _firmaCalisan = new Context.FirmaCalisani();

                    _firmaCalisan.UserName = _firmaCalisanBilgileri.UserName;
                    _firmaCalisan.Password = _firmaCalisanBilgileri.Password;
                    _firmaCalisan.Ad = _firmaCalisanBilgileri.Ad;
                    _firmaCalisan.Soyad = _firmaCalisanBilgileri.Soyad;
                    _firmaCalisan.Gsm = _firmaCalisanBilgileri.Gsm;
                    _firmaCalisan.Email = _firmaCalisanBilgileri.Email;
                    _firmaCalisan.FirmaID = _firmaCalisanBilgileri.FirmaID;
                    _firmaCalisan.CreateUserID = _firmaCalisanBilgileri.CreateUserID;
                    _firmaCalisan.CreateDate = DateTime.Now;

                    dbFirmaYonetici.FirmaCalisani.Add(_firmaCalisan);
                    dbFirmaYonetici.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(_firmaCalisanBilgileri);
            }
        }

        private void ListYarat()
        {
            CalisanBilgileri.firmaCalisanList.Clear();

            int temp, passLength = 0, count = 0;

            count = dbFirmaYonetici.FirmaCalisani.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (Connection.parentID == dbFirmaYonetici.FirmaCalisani.ToList()[temp].FirmaID)
                {
                    var _firmaCalisanList = new CalisanBilgileri();

                    _firmaCalisanList.FcID = dbFirmaYonetici.FirmaCalisani.ToList()[temp].FcID;
                    _firmaCalisanList.UserName = dbFirmaYonetici.FirmaCalisani.ToList()[temp].UserName;
                    _firmaCalisanList.Password = dbFirmaYonetici.FirmaCalisani.ToList()[temp].Password;
                    _firmaCalisanList.Ad = dbFirmaYonetici.FirmaCalisani.ToList()[temp].Ad;
                    _firmaCalisanList.Soyad = dbFirmaYonetici.FirmaCalisani.ToList()[temp].Soyad;
                    _firmaCalisanList.Gsm = dbFirmaYonetici.FirmaCalisani.ToList()[temp].Gsm;
                    _firmaCalisanList.Email = dbFirmaYonetici.FirmaCalisani.ToList()[temp].Email;
                    _firmaCalisanList.FirmaID = dbFirmaYonetici.FirmaCalisani.ToList()[temp].FirmaID;
                    _firmaCalisanList.CreateUserID = dbFirmaYonetici.FirmaCalisani.ToList()[temp].CreateUserID;

                    passLength = (dbFirmaYonetici.FirmaCalisani.ToList()[temp].Password).Length;

                    for (int temp2 = 0; temp2 < passLength; temp2++)
                    {
                        _firmaCalisanList.YoneteciPassword2 += "*";
                    }

                    _firmaCalisanList.CreateDate = dbFirmaYonetici.FirmaCalisani.ToList()[temp].CreateDate;

                    CalisanBilgileri.firmaCalisanList.Add(_firmaCalisanList);
                }
            }
        }

        private void ListTemizle()
        {
            CalisanBilgileri.firmaCalisanList.Clear();
        }

    }
}
