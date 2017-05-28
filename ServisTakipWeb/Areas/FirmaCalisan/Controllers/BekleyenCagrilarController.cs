using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaCalisan.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaCalisan.Controllers
{
    public class BekleyenCagrilarController : BaseController
    {
        //
        // GET: /FirmaCalisan/BekleyenCagrilar/

        public ActionResult Index()
        {
            AtanmisCagrilarListYarat();

            return View(AtanmisCagrilar.cagriAtanmisList);
        }

        public ActionResult Goruntule(int _atanmisCagriNo = -1)
        {
            var atanmisCagri = AtanmisCagrilar.cagriAtanmisList.SingleOrDefault(x => x.CagriNo == _atanmisCagriNo);

            if (atanmisCagri != null)
                return View(atanmisCagri);
            else
                return View("Index");
        }

        public ActionResult Tamamla(int _bekleyenCagriNo = -1)
        {
            var cagri = AtanmisCagrilar.cagriAtanmisList.SingleOrDefault(x => x.CagriNo == _bekleyenCagriNo);
            var musteri = dbFirmaYonetici.Musteri.SingleOrDefault(x => x.MusteriKodu == cagri.MusteriKodu);
            var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == musteri.ID);
            var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(x => x.ID == sozlesmeYapma.SozlesmeID);

            var cagriTamamla = new ServisTakipWeb.Areas.FirmaCalisan.Models.CagriTamamlamaBilgileri();

            cagriTamamla.MusteriSozlesmeParcaDahilMi = sozlesme.ParcaDahilMi;

            cagriTamamla.TamamlayanYoneticiID = -1; //Firma Calisan Panelindeyiz.
            cagriTamamla.TamamlayanCalisanID = Connection.ID; //Firma Calisan Panelindeyiz.
            cagriTamamla.MusteriID = musteri.ID;
            cagriTamamla.Sonuc = "";
            cagriTamamla.Durum = "tamamlanıyor.";
            cagriTamamla.Email = cagri.Email;
            cagriTamamla.FormNo = "";
            cagriTamamla.MusteriAdi = cagri.MusteriAdi;
            cagriTamamla.YetkiliKisi = cagri.IlgiliKisi;
            cagriTamamla.Telefon = cagri.Telefon;
            cagriTamamla.Adres = cagri.Adres;
            cagriTamamla.VergiDairesi = musteri.VergiDairesi;
            cagriTamamla.VergiNumarasi = musteri.VergiNumarasi;
            cagriTamamla.MusteriKodu = cagri.MusteriKodu;
            cagriTamamla.BildirilenAriza = cagri.Aciklama + " - " + cagri.CagriDetayi;
            cagriTamamla.HizmetTipi = "";
            cagriTamamla.CihazinHizmetDurumu = "";
            cagriTamamla.Marka1 = "";
            cagriTamamla.Marka2 = "";
            cagriTamamla.Marka3 = "";
            cagriTamamla.Marka4 = "";
            cagriTamamla.Model1 = "";
            cagriTamamla.Model2 = "";
            cagriTamamla.Model3 = "";
            cagriTamamla.Model4 = "";
            cagriTamamla.SeriNo1 = "";
            cagriTamamla.SeriNo2 = "";
            cagriTamamla.SeriNo3 = "";
            cagriTamamla.SeriNo4 = "";

            cagriTamamla.CagriBildirildigiTarih = cagri.CagriAcilisTarihi;
            cagriTamamla.HizmetBaslangicTarihi = DateTime.Now;
            cagriTamamla.HizmetBitisTarihi = DateTime.Now;
            cagriTamamla.CagriKayitNo = cagri.CagriNo;
            cagriTamamla.MesaiSaatiIcindeMi = true;

            cagriTamamla.YapilanIsinAciklamasi = "";
            cagriTamamla.ParcaNo1 = "";
            cagriTamamla.ParcaNo2 = "";
            cagriTamamla.ParcaNo3 = "";
            cagriTamamla.ParcaAdi1 = "";
            cagriTamamla.ParcaAdi2 = "";
            cagriTamamla.ParcaAdi3 = "";
            cagriTamamla.Miktar1 = 0;
            cagriTamamla.Miktar2 = 0;
            cagriTamamla.Miktar3 = 0;
            cagriTamamla.BirimFiyati1 = 0;
            cagriTamamla.BirimFiyati2 = 0;
            cagriTamamla.BirimFiyati3 = 0;

            cagriTamamla.AciklamaIscilik1 = "";
            cagriTamamla.AciklamaIscilik2 = "";
            cagriTamamla.AciklamaIscilik3 = "";
            cagriTamamla.Sure1 = 0;
            cagriTamamla.Sure2 = 0;
            cagriTamamla.Sure3 = 0;
            cagriTamamla.BirimFiyatiIscilik1 = 0;
            cagriTamamla.BirimFiyatiIscilik2 = 0;
            cagriTamamla.BirimFiyatiIscilik3 = 0;

            if (cagriTamamla == null)
                return View("Index");
            else
                return View(cagriTamamla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tamamla(ServisTakipWeb.Areas.FirmaCalisan.Models.CagriTamamlamaBilgileri _cagriTamamlama)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cagriTamamlama.Sonuc = "Tamamlandı";

                    var cagriTamamla = new FirmaYonetici.Context.TamamlananCagrilar();

                    cagriTamamla.TamamlayanYoneticiID = -1; //Firma Yonetici Panelindeyiz.
                    cagriTamamla.TamamlayanCalisanID = Connection.ID; //Firma Yonetici Panelindeyiz.
                    cagriTamamla.FormNo = _cagriTamamlama.FormNo;
                    cagriTamamla.MID = _cagriTamamlama.MusteriID;
                    cagriTamamla.YetkiliKisi = _cagriTamamlama.YetkiliKisi;
                    cagriTamamla.Gsm = _cagriTamamlama.Telefon;
                    cagriTamamla.Email = _cagriTamamlama.Email;
                    cagriTamamla.BildirilenAriza = _cagriTamamlama.BildirilenAriza;
                    cagriTamamla.HizmetTipi = _cagriTamamlama.HizmetTipi;
                    cagriTamamla.CihazinHizmetDurumu = _cagriTamamlama.CihazinHizmetDurumu;
                    cagriTamamla.CagrininBildirigiTarih = _cagriTamamlama.CagriBildirildigiTarih;
                    cagriTamamla.HizmetBaslangicTarihi = _cagriTamamlama.HizmetBaslangicTarihi;
                    cagriTamamla.HizmetBitisTarihi = _cagriTamamlama.HizmetBitisTarihi;
                    cagriTamamla.CagriKayitNo = _cagriTamamlama.CagriKayitNo;
                    cagriTamamla.MesaiSaatiIcindeMi = _cagriTamamlama.MesaiSaatiIcindeMi;
                    cagriTamamla.YapılanIsinAciklamasi = _cagriTamamlama.YapilanIsinAciklamasi;
                    cagriTamamla.Sonuc = _cagriTamamlama.Sonuc;
                    cagriTamamla.CreateDate = DateTime.Now;
                    cagriTamamla.AnketYapildiMi = false;

                    /*cagriTamamla.Marka1 = "";
                    cagriTamamla.Marka2 = "";
                    cagriTamamla.Marka3 = "";
                    cagriTamamla.Marka4 = "";
                    cagriTamamla.Model1 = "";
                    cagriTamamla.Model2 = "";
                    cagriTamamla.Model3 = "";
                    cagriTamamla.Model4 = "";
                    cagriTamamla.SeriNo1 = "";
                    cagriTamamla.SeriNo2 = "";
                    cagriTamamla.SeriNo3 = "";
                    cagriTamamla.SeriNo4 = "";*/

                    /*cagriTamamla.ParcaNo1 = "";
                    cagriTamamla.ParcaNo2 = "";
                    cagriTamamla.ParcaNo3 = "";
                    cagriTamamla.ParcaAdi1 = "";
                    cagriTamamla.ParcaAdi2 = "";
                    cagriTamamla.ParcaAdi3 = "";
                    cagriTamamla.Miktar1 = 0;
                    cagriTamamla.Miktar2 = 0;
                    cagriTamamla.Miktar3 = 0;
                    cagriTamamla.BirimFiyati1 = 0;
                    cagriTamamla.BirimFiyati2 = 0;
                    cagriTamamla.BirimFiyati3 = 0;

                    cagriTamamla.AciklamaIscilik1 = "";
                    cagriTamamla.AciklamaIscilik2 = "";
                    cagriTamamla.AciklamaIscilik3 = "";
                    cagriTamamla.Sure1 = 0;
                    cagriTamamla.Sure2 = 0;
                    cagriTamamla.Sure3 = 0;
                    cagriTamamla.BirimFiyatiIscilik1 = 0;
                    cagriTamamla.BirimFiyatiIscilik2 = 0;
                    cagriTamamla.BirimFiyatiIscilik3 = 0;*/

                    //bool kayitBasarili = false;

                    dbFirmaYonetici.TamamlananCagrilar.Add(cagriTamamla);
                    dbFirmaYonetici.SaveChanges();

                    //Trigger ile yapildi.

                    //kayitBasarili = true;

                    //if (kayitBasarili == true)
                    //{
                    //    var acilanCagri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == cagriTamamla.CagriKayitNo);
                    //    var atananCagri = dbFirmaYonetici.AtananCagrilar.SingleOrDefault(x => x.CagriNo == cagriTamamla.CagriKayitNo);
                    //    var bekleyenCagri = dbFirmaYonetici.BekleyenCagrilar.SingleOrDefault(x=>x.CagriNo == cagriTamamla.CagriKayitNo); 

                    //    dbFirmaYonetici.AtananCagrilar.Remove(atananCagri);
                    //    dbFirmaYonetici.SaveChanges();

                    //    dbFirmaYonetici.BekleyenCagrilar.Remove(bekleyenCagri);
                    //    dbFirmaYonetici.SaveChanges();

                    //    dbMusteriCalisan.AcilanCagri.Remove(acilanCagri);
                    //    dbMusteriCalisan.SaveChanges();
                    //}

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View(_cagriTamamlama);
            }
            //TODO: cagri tamamlama ekrani geri donen degerlere tek tek bak. Kontrol et
            return View();
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

                AtanmisCagrilar.cagriAtanmisList.Add(atanmisCagrilar);
            }

            AtanmisCagrilar.cagriAtanmisList = AtanmisCagrilar.cagriAtanmisList.OrderBy(x => x.CreateDate).ToList();
        }
    }
}
