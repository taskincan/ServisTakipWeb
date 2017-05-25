using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class BekleyenCagrilarController : BaseController
    {
        //
        // GET: /FirmaYonetici/BekleyenCagrilar/

        public ActionResult Index()
        {
            AtanmisCagrilarListYarat();

            return View(AtanmisCagriBilgileri.cagriAtanmisList);
        }

        public ActionResult Goruntule(int _atanmisCagriNo = -1)
        {
            var atanmisCagri = AtanmisCagriBilgileri.cagriAtanmisList.SingleOrDefault(x => x.CagriNo == _atanmisCagriNo);

            if (atanmisCagri != null)
                return View(atanmisCagri);
            else
                return View("Index");
        }

        private void AtanmisCagrilarListYarat()
        {
            AtanmisCagriBilgileri.cagriAtanmisList.Clear();

            var atananCagri = dbFirmaYonetici.AtananCagrilar.Where(x => x.FirmaYonetici.FirmaID == Connection.parentID).ToList();

            int count = 0, temp = 0, cagriNo = 0, atayanID = 0;

            count = atananCagri.Count();

            for (temp = 0; temp < count; temp++)
            {
                var atanmisCagrilar = new AtanmisCagriBilgileri();

                cagriNo = atananCagri.ToList()[temp].CagriNo;
                atayanID = atananCagri.ToList()[temp].AtayanID;

                var cagriBilgileri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == cagriNo);
                var musteriCalisan = dbMusteri.MusteriCalisani.Single(x => x.McID == cagriBilgileri.McID);
                var musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == musteriCalisan.MusteriID); 
                var bekleyenCagri = dbFirmaYonetici.BekleyenCagrilar.SingleOrDefault(x => x.CagriNo == cagriNo);

                if (bekleyenCagri == null)
                {
                    atanmisCagrilar.Durum = "Atanan Çağrı";
                }
                else
                {
                    atanmisCagrilar.Durum = "Bekleyen Çağrı";
                    atanmisCagrilar.BeklemeyeAlmaNedeni = bekleyenCagri.BeklemeNedeni;
                }
                 
                atanmisCagrilar.AtananID = atananCagri.ToList()[temp].AtananID;
                atanmisCagrilar.AtayanID = atananCagri.ToList()[temp].AtayanID;
                atanmisCagrilar.AtayanAdiSoyadi = atananCagri.ToList()[temp].FirmaYonetici.Ad + " " + atananCagri.ToList()[temp].FirmaYonetici.Soyad;
                atanmisCagrilar.AtananAdiSoyadi = atananCagri.ToList()[temp].FirmaCalisani.Ad + " " + atananCagri.ToList()[temp].FirmaCalisani.Soyad;

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

                AtanmisCagriBilgileri.cagriAtanmisList.Add(atanmisCagrilar);
            }

            AtanmisCagriBilgileri.cagriAtanmisList = AtanmisCagriBilgileri.cagriAtanmisList.OrderBy(x => x.CreateDate).ToList();
        }
    }
}
