﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.MusteriCalisan.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.MusteriCalisan.Controllers
{
    public class CagriOlusturController : BaseController
    {
        //
        // GET: /MusteriCalisan/CagriOlustur/

        public ActionResult Index()
        {
            CagriBilgileri cagri = new CagriBilgileri();

            var _musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == Connection.parentID);
            var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(c => c.MID == _musteri.ID);
            var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(x => x.ID == sozlesmeYapma.SozlesmeID);

            cagri.CagriNo = CagriNoYarat();

            cagri.Adres = _musteri.Adres;
            cagri.MusteriAdi = _musteri.MusteriAdi;
            cagri.Sozlesme = sozlesme.SozlesmeAdi;
            cagri.CagriAcilisTarihi = DateTime.Now;

            cagri.IlgiliKisi = "";
            cagri.Telefon = "";
            cagri.Email = "";
            cagri.CihazTipi = "";
            cagri.Marka = "";
            cagri.Model = "";
            cagri.SeriNo = "";
            cagri.BarkodNo = "";
            cagri.Aciklama = "";
            cagri.CagriDetayi = "";
            cagri.SarfMalzemeTalebi = "";
            cagri.CreateDate = DateTime.Now;
            cagri.CreateUserID = Connection.parentID;
            cagri.IslemGorduMu = false;

            return View(cagri);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CagriBilgileri _cagri)
        {
            try    
            {
                if (ModelState.IsValid)
                {
                    var cagri = new Context.AcilanCagri();

                    cagri.CagriNo = _cagri.CagriNo;

                    cagri.YetkiliKisi = _cagri.IlgiliKisi;
                    cagri.Gsm = _cagri.Telefon;
                    cagri.Email = _cagri.Email;
                    cagri.CihazTipi = _cagri.CihazTipi;
                    cagri.Marka = _cagri.Marka;
                    cagri.Model = _cagri.Model;
                    cagri.SeriNo = _cagri.SeriNo;
                    cagri.BarkodNo = _cagri.BarkodNo;
                    cagri.Aciklama = _cagri.Aciklama;
                    cagri.CagriDetayi = _cagri.CagriDetayi;
                    cagri.SarfMalzemeTalebi = _cagri.SarfMalzemeTalebi;
                    cagri.AcilisTarihi = DateTime.Now;
                    cagri.McID = Connection.ID;
                    cagri.IslemGorduMu = false;

                    dbMusteriCalisan.AcilanCagri.Add(cagri);
                    dbMusteriCalisan.SaveChanges();

                    if(cagri.Email != "")
                    {
                        var body = new StringBuilder();
                        body.AppendLine("Çağrı kaydınız başarıyla oluşturulmuştur.");
                        body.AppendLine(" ");
                        body.AppendLine("Çağrı Numaranız : " + _cagri.CagriNo);
                        body.AppendLine("Yetkili Kişi : " + _cagri.IlgiliKisi);
                        body.AppendLine("Telefon Numarası: " + _cagri.Telefon);
                        body.AppendLine("Email : " + _cagri.Email);
                        body.AppendLine("Cihaz Tipi : " + _cagri.CihazTipi);
                        body.AppendLine("Marka" + _cagri.Marka);
                        body.AppendLine("Model : " + _cagri.Model);
                        body.AppendLine("Seri No :" + _cagri.SeriNo);
                        body.AppendLine("Barkod No : " + _cagri.BarkodNo);
                        body.AppendLine("Açıklama : " + _cagri.Aciklama);
                        body.AppendLine("Çağrı Detayı : " + _cagri.CagriDetayi);
                        body.AppendLine("Sarf Malzeme Talebi : " + _cagri.SarfMalzemeTalebi);
                        body.AppendLine("Çağrı Açılış Tarihi : " + cagri.AcilisTarihi.ToShortDateString());
                         
                        MailSender(body.ToString(), cagri.Email.ToString());
                    }
                     
                    return RedirectToAction("Index", "AnaSayfa");
                }
                return View(_cagri);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Mail adresiniz yanlış olduğu için mail gönderilememişstir");

                return View(_cagri);
            } 
        }

       

        private int CagriNoYarat()
        {
            int sayi = 0;
            Random rastgele = new Random();

            sayi = rastgele.Next(10000000, 19999999);
            //TODO: Tamamlanan çağrılarda ki cagri no larına ve acilan cagrilada ki cagrinolarına bakarak yapacak burayı.

            return sayi;

        }

    }
}
