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
    public class CalisanBilgileriController : BaseController
    {
        //
        // GET: /Musteri/CalisanBilgileri/

        public ActionResult Index()
        {
            ListTemizle();
            ListYarat();

            Connection.sayfaAdi = "Yönetici Bilgileri";

            return View(MusteriCalisanBilgileri.musteriCalisanList); 
        }

        public ActionResult Edit(int id = 0)
        {
            var _musteriCalisan = MusteriCalisanBilgileri.musteriCalisanList.SingleOrDefault(x => x.McID == id);

            if (_musteriCalisan == null)
            {
                return RedirectToAction("Index");
            }

            return View(_musteriCalisan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MusteriCalisanBilgileri _musteriCalisan)
        {
            if (ModelState.IsValid)
            {
                var _user = dbMusteri.MusteriCalisani.SingleOrDefault(x => x.McID == _musteriCalisan.McID);

                _user.McID = _musteriCalisan.McID;
                _user.UserName = _musteriCalisan.UserName;
                _user.Password = _musteriCalisan.Password;
                _user.MusteriID = _musteriCalisan.MusteriID;
                _user.CreateDate = DateTime.Now;

                dbMusteri.Entry(_user).State = EntityState.Modified;
                dbMusteri.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(_musteriCalisan);
        }



        private void ListYarat()
        {
            MusteriCalisanBilgileri.musteriCalisanList.Clear();

            int temp, passLength = 0, count = 0;
            count = dbMusteri.MusteriCalisani.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (Connection.ID == dbMusteri.MusteriCalisani.ToList()[temp].MusteriID)
                {
                    var _musteriCalisan = new MusteriCalisanBilgileri();
                    var musteriCalisan = dbMusteri.MusteriCalisani.ToList()[temp];

                    _musteriCalisan.McID = musteriCalisan.McID;
                    _musteriCalisan.UserName = musteriCalisan.UserName;
                    _musteriCalisan.Password = musteriCalisan.Password;
                    _musteriCalisan.MusteriID = musteriCalisan.MusteriID;

                    passLength = (musteriCalisan.Password).Length;

                    for (int temp2 = 0; temp2 < passLength; temp2++)
                    {
                        _musteriCalisan.Password2 += "*";
                    }

                    _musteriCalisan.CreateDate = musteriCalisan.CreateDate;

                    MusteriCalisanBilgileri.musteriCalisanList.Add(_musteriCalisan);
                }
            }
        }

        private void ListTemizle()
        {
            MusteriCalisanBilgileri.musteriCalisanList.Clear();
        }

    }
}
