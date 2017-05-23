using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaCalisan.Models;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaCalisan.Controllers
{
    public class AtanmisCagrilarController : BaseController
    {
        //
        // GET: /FirmaCalisan/AtanmisCagrilar/

        public ActionResult Index()
        {
            AtanmisCagrilarListYarat();
             
            return View(AtanmisCagrilar.cagriAtanmisList);
        }

        public ActionResult Goruntule(int _atanmisCagriNo = -1)
        {
            var atanmisCagri = AtanmisCagrilar.cagriAtanmisList.SingleOrDefault(x=>x.CagriNo == _atanmisCagriNo);
            
            if(atanmisCagri !=null)
                return View(atanmisCagri);  
            else 
                return View("Index");
        }



        private void AtanmisCagrilarListYarat()
        {
            AtanmisCagrilar.cagriAtanmisList.Clear();

            var atananCagri = dbFirmaYonetici.AtananCagrilar.Where(x => x.AtananID == Connection.ID).ToList();
             
            int count = 0, temp = 0, cagriNo = 0, atayanID = 0;

            count = atananCagri.Count();

            for (temp = 0; temp < count; temp++)
            {
                var atanmisCagrilar = new AtanmisCagrilar();

                cagriNo = atananCagri.ToList()[temp].CagriNo;
                atayanID = atananCagri.ToList()[temp].AtayanID;

                var cagriBilgileri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == cagriNo);

                var musteriCalisan = dbMusteri.MusteriCalisani.Single(x => x.McID == cagriBilgileri.McID);
                var musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == musteriCalisan.MusteriID);
                var yonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(x => x.FyID == atayanID);

                atanmisCagrilar.AtananID = atananCagri.ToList()[temp].AtananID;
                atanmisCagrilar.AtayanID = atananCagri.ToList()[temp].AtayanID;
                atanmisCagrilar.AtayanAdiSoyadi = yonetici.Ad + " " + yonetici.Soyad;
                atanmisCagrilar.CagriNo = atananCagri.ToList()[temp].CagriNo;
                atanmisCagrilar.AcilMi = Convert.ToBoolean(atananCagri.ToList()[temp].Aciliyet);
                atanmisCagrilar.CreateDate = atananCagri.ToList()[temp].CreateDate;
                atanmisCagrilar.VarisTarih = atananCagri.ToList()[temp].VarisTarih;
                atanmisCagrilar.Not = atananCagri.ToList()[temp].YoneticiNotu;

                atanmisCagrilar.ID = cagriBilgileri.ID;
                atanmisCagrilar.Adres = musteri.Adres;
                atanmisCagrilar.MusteriAdi = musteri.MusteriAdi;
                atanmisCagrilar.MusteriKodu = musteri.MusteriKodu;
                atanmisCagrilar.CagriAcilisTarihi = cagriBilgileri.AcilisTarihi;

                atanmisCagrilar.IlgiliKisi = cagriBilgileri.YetkiliKisi;
                atanmisCagrilar.Telefon = cagriBilgileri.Gsm;
                atanmisCagrilar.Email = cagriBilgileri.Email;
                atanmisCagrilar.CihazTipi = cagriBilgileri.CihazTipi;
                atanmisCagrilar.Marka = cagriBilgileri.Marka;
                atanmisCagrilar.Model = cagriBilgileri.Model;
                atanmisCagrilar.SeriNo = cagriBilgileri.SeriNo;
                atanmisCagrilar.BarkodNo = cagriBilgileri.BarkodNo;
                atanmisCagrilar.Aciklama = cagriBilgileri.Aciklama;
                atanmisCagrilar.CagriDetayi = cagriBilgileri.CagriDetayi;
                atanmisCagrilar.SarfMalzemeTalebi = cagriBilgileri.SarfMalzemeTalebi;


                atanmisCagrilar.Durum = "Atanmış Çağrı";

                AtanmisCagrilar.cagriAtanmisList.Add(atanmisCagrilar);
            }

            AtanmisCagrilar.cagriAtanmisList = AtanmisCagrilar.cagriAtanmisList.OrderBy(x => x.CreateDate).ToList();
        }
          


    }
}
