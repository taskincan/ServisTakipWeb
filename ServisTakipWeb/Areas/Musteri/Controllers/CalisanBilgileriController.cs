using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

            Connection.sayfaAdi = "Çalışan Bilgileri";

            return View(MusteriCalisanBilgileri.musteriCalisanList);
        }

        public ActionResult SifreYolla(int id = -1)
        {
            var calisan = MusteriCalisanBilgileri.musteriCalisanList.SingleOrDefault(x => x.McID == id);
            var Musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == calisan.MusteriID);

            string password = "";
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            password = finalString;

            string passwordYeni = MD5Hash(password);

            var _user = dbMusteri.MusteriCalisani.SingleOrDefault(x => x.McID == calisan.McID);
              
            _user.McID = calisan.McID;
            _user.Password = passwordYeni;
            _user.MusteriID = calisan.MusteriID;

            dbMusteri.Entry(_user).State = EntityState.Modified;
            dbMusteri.SaveChanges();
            ModelState.Clear();
             
            if (Musteri.Email != "")
            {
                var body = new StringBuilder(); 
                body.AppendLine("Müşteri Çalışanı");
                body.AppendLine(" ");
                body.AppendLine("Kullanıcı Adınız : " + calisan.UserName);
                body.AppendLine("Yeni Şifreniz : " + password);

                MailSender(body.ToString(), Musteri.Email.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult SifreYenile(int id = -1)
        {
            var calisan = MusteriCalisanBilgileri.musteriCalisanList.SingleOrDefault(x => x.McID == id);

            if (calisan != null)
                return View(calisan);
            else
                return View("Index", "CalisanBilgileri");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SifreYenile(MusteriCalisanBilgileri _musteriCalisan)
        {
            try
            {
                string passwordMD5 = MD5Hash(_musteriCalisan.Password);

                var calisan = MusteriCalisanBilgileri.musteriCalisanList.SingleOrDefault(x => x.McID == _musteriCalisan.McID);

                if (calisan.Password == passwordMD5) // sifre degistirilebilir.
                {
                    var _user = dbMusteri.MusteriCalisani.SingleOrDefault(x => x.McID == _musteriCalisan.McID);

                    string passwordYeniMD5 = MD5Hash(_musteriCalisan.PasswordYeni);

                    _user.McID = _musteriCalisan.McID;
                    _user.Password = passwordYeniMD5;
                    _user.MusteriID = _musteriCalisan.MusteriID;

                    dbMusteri.Entry(_user).State = EntityState.Modified;
                    dbMusteri.SaveChanges();
                    ModelState.Clear();

                    return RedirectToAction("Index");
                }
                else // girilen sifre yanlis
                {
                    ViewBag.Message = "Şifrenizi kontrol ediniz.";
                    return View(_musteriCalisan);
                } 
            }
            catch (Exception ex)
            {
                return View();
            }
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

        public ActionResult Create()
        {
            int count = 0;

            count = MusteriCalisanBilgileri.musteriCalisanList.Count();

            if (count == 0) // Yeni Kayıt Yapılabilir.
            {
                var musteri = new MusteriCalisanBilgileri();

                return View(musteri);
            }
            else // Yeni kayıt yapılamaz. Çünkü 1 adet çalışan var.
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusteriCalisanBilgileri _musteriCalisan)
        {
            var musteriCalisan = new Context.MusteriCalisani();

            string PasswordMD5 = MD5Hash(_musteriCalisan.Password.ToString());

            musteriCalisan.UserName = _musteriCalisan.UserName.Trim();
            musteriCalisan.Password = PasswordMD5;
            musteriCalisan.MusteriID = Connection.ID;
            musteriCalisan.CreateDate = DateTime.Now;

            dbMusteri.MusteriCalisani.Add(musteriCalisan);
            dbMusteri.SaveChanges();

            return RedirectToAction("Index");
        }

        private void ListYarat()
        {
            MusteriCalisanBilgileri.musteriCalisanList.Clear();

            int temp, count = 0;
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
                    _musteriCalisan.Password2 = "****";
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
