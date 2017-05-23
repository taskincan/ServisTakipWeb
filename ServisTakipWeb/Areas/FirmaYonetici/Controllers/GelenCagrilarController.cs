using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class GelenCagrilarController : BaseController
    {
        //
        // GET: /FirmaYonetici/GelenCagrilar/

        public ActionResult Index()
        {
            GelenCagriListYarat();
             
  
            Connection.sayfaAdi = "Gelen Çağrılar";
            return View(CagriBilgileri.cagriList);
        }

        public ActionResult Goruntule(int _cagriNo = -1)
        {
            var cagri = CagriBilgileri.cagriList.SingleOrDefault(x => x.CagriNo == _cagriNo);
             
            if (cagri == null)
                return View("Index");
            else
                return View(cagri);
        }

        public ActionResult Tamamla(int _cagriNo = -1)
        {
            var cagri = CagriBilgileri.cagriList.SingleOrDefault(x => x.CagriNo == _cagriNo);
            var musteri = dbFirmaYonetici.Musteri.SingleOrDefault(x => x.MusteriKodu == cagri.MusteriKodu);
            var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == musteri.ID);
            var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(x => x.ID == sozlesmeYapma.SozlesmeID);

            var cagriTamamla = new CagriTamamlamaBilgileri();

            cagriTamamla.MusteriSozlesmeParcaDahilMi = sozlesme.ParcaDahilMi;

            cagriTamamla.TamamlayanYoneticiID = Connection.ID; //Firma Yonetici Panelindeyiz.
            cagriTamamla.TamamlayanCalisanID = -1; //Firma Yonetici Panelindeyiz.
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
        public ActionResult Tamamla(CagriTamamlamaBilgileri _cagriTamamlama)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cagriTamamlama.Sonuc = "Tamamlandı";

                    var cagriTamamla = new Context.TamamlananCagrilar();

                    cagriTamamla.TamamlayanYoneticiID = Connection.ID; //Firma Yonetici Panelindeyiz.
                    cagriTamamla.TamamlayanCalisanID = -1; //Firma Yonetici Panelindeyiz.
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
                    cagriTamamla.AnketYapildiMi = _cagriTamamlama.AnketYapildiMi;

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

                    bool kayitBasarili = false;

                    dbFirmaYonetici.TamamlananCagrilar.Add(cagriTamamla);
                    dbFirmaYonetici.SaveChanges();

                    kayitBasarili = true;

                    if (kayitBasarili == true)
                    {
                        var acilanCagri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == cagriTamamla.CagriKayitNo);

                        dbMusteriCalisan.AcilanCagri.Remove(acilanCagri);
                        dbMusteriCalisan.SaveChanges();
                    }

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
         
        public ActionResult Atama(int _cagriNo = -1)
        {  
            var _cagri = CagriBilgileri.cagriList.SingleOrDefault(x => x.CagriNo == _cagriNo);
            var cagriAtama = new CagriAtamaBilgileri();
            
            CalisanListYarat();

            CagriAtamaBilgileri.calisanList = CalisanBilgileri.firmaCalisanList.ToList();
            
            cagriAtama.AtananID = 1;

            cagriAtama.CagriNo = _cagri.CagriNo;
            cagriAtama.MusteriAdi = _cagri.MusteriAdi;
            cagriAtama.Adres = _cagri.Adres;
            cagriAtama.MusteriKodu = _cagri.MusteriKodu;
            cagriAtama.IlgiliKisi = _cagri.IlgiliKisi;
            cagriAtama.Telefon = _cagri.Telefon;
            cagriAtama.Email = _cagri.Email;
            cagriAtama.CagriAcilisTarihi = _cagri.CagriAcilisTarihi;
            cagriAtama.CihazTipi = _cagri.CihazTipi;
            cagriAtama.Marka = _cagri.Marka;
            cagriAtama.Model = _cagri.Model;
            cagriAtama.SeriNo = _cagri.SeriNo;
            cagriAtama.BarkodNo = _cagri.BarkodNo;
            cagriAtama.Aciklama = _cagri.Aciklama;
            cagriAtama.CagriDetayi = _cagri.CagriDetayi;
            cagriAtama.SarfMalzemeTalebi = _cagri.SarfMalzemeTalebi;
            cagriAtama.AtayanID = Connection.ID;
            cagriAtama.AtananID = 0;
            cagriAtama.Not = "";
            cagriAtama.VarisTarih = DateTime.Now;
            cagriAtama.AcilMi = false;

            ViewBag.CalisanList = CagriAtamaBilgileri.calisanList;

            if (cagriAtama == null)
                return View("Index");
            else
                return View(cagriAtama);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atama(CagriAtamaBilgileri _cagriAtama)
        {
            try
            {
                if (ModelState.IsValid)
                {   
                    var cagriAtama = new Context.AtananCagrilar();
                     
                    cagriAtama.Aciliyet = _cagriAtama.AcilMi;
                    cagriAtama.AtananID = _cagriAtama.AtananID;
                    cagriAtama.AtayanID = Connection.ID;
                    cagriAtama.CagriNo = _cagriAtama.CagriNo;
                    cagriAtama.CreateDate = DateTime.Now;
                    cagriAtama.VarisTarih = _cagriAtama.VarisTarih;
                    cagriAtama.YoneticiNotu = _cagriAtama.Not; 

                    bool kayitBasarili = false;

                    dbFirmaYonetici.AtananCagrilar.Add(cagriAtama);
                    dbFirmaYonetici.SaveChanges();

                    kayitBasarili = true;

                    if (kayitBasarili == true)
                    {
                        var acilanCagri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == _cagriAtama.CagriNo);

                        acilanCagri.IslemGorduMu = true; 
                        
                        dbMusteriCalisan.Entry(acilanCagri).State = EntityState.Modified;
                        dbMusteriCalisan.SaveChanges(); 
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(_cagriAtama);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View();
            }
            return View(_cagriAtama);
        }
         
        public ActionResult CagriIptal(int _cagriNo = -1)
        {
            var _cagri = CagriBilgileri.cagriList.SingleOrDefault(x => x.CagriNo == _cagriNo);

            var cagriIptal = new CagriIptalBilgileri();

            cagriIptal.CagriNo = _cagri.CagriNo;
            cagriIptal.MusteriAdi = _cagri.MusteriAdi;
            cagriIptal.Adres = _cagri.Adres;
            cagriIptal.MusteriKodu = _cagri.MusteriKodu;
            cagriIptal.IlgiliKisi = _cagri.IlgiliKisi;
            cagriIptal.Telefon = _cagri.Telefon;
            cagriIptal.Email = _cagri.Email;
            cagriIptal.CagriAcilisTarihi = _cagri.CagriAcilisTarihi;
            cagriIptal.CihazTipi = _cagri.CihazTipi;
            cagriIptal.Marka = _cagri.Marka;
            cagriIptal.Model = _cagri.Model;
            cagriIptal.SeriNo = _cagri.SeriNo;
            cagriIptal.BarkodNo = _cagri.BarkodNo;
            cagriIptal.Aciklama = _cagri.Aciklama;
            cagriIptal.CagriDetayi = _cagri.CagriDetayi;
            cagriIptal.SarfMalzemeTalebi = _cagri.SarfMalzemeTalebi;
            cagriIptal.TamamlayanYoneticiID = Connection.ID;

            return View(cagriIptal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CagriIptal(CagriIptalBilgileri _cagriIptal)
        {
            try
            {
                var _cagri = CagriBilgileri.cagriList.SingleOrDefault(x => x.CagriNo == _cagriIptal.CagriNo);

                if (ModelState.IsValid)
                {
                    var cagriIptal = new Context.TamamlananCagrilar();

                    cagriIptal.FormNo = _cagriIptal.FormNo;
                    cagriIptal.YapılanIsinAciklamasi = _cagriIptal.CagriIptalEtmeNedeni;

                    cagriIptal.CagriKayitNo = _cagri.CagriNo;
                    cagriIptal.MID = _cagri.CreateUserID;
                    cagriIptal.YetkiliKisi = _cagri.IlgiliKisi;
                    cagriIptal.Gsm = _cagri.Telefon;
                    cagriIptal.Email = _cagri.Email;
                    cagriIptal.BildirilenAriza = _cagri.CagriDetayi + " - " + _cagri.Aciklama;

                    _cagri.Durum = "İptal";

                    cagriIptal.TamamlayanYoneticiID = Connection.ID; //Firma Yonetici Panelindeyiz.
                    cagriIptal.TamamlayanCalisanID = -1; //Firma Yonetici Panelindeyiz.
                    cagriIptal.HizmetTipi = "-";
                    cagriIptal.CihazinHizmetDurumu = "-";
                    cagriIptal.CagrininBildirigiTarih = _cagri.CagriAcilisTarihi;
                    cagriIptal.HizmetBaslangicTarihi = DateTime.Now;
                    cagriIptal.HizmetBitisTarihi = DateTime.Now; 
                    cagriIptal.MesaiSaatiIcindeMi = true;
                    cagriIptal.Sonuc = _cagri.Durum;
                    cagriIptal.CreateDate = DateTime.Now;
                    cagriIptal.AnketYapildiMi = false;

                    bool kayitBasarili = false;

                    dbFirmaYonetici.TamamlananCagrilar.Add(cagriIptal);
                    dbFirmaYonetici.SaveChanges();

                    kayitBasarili = true;

                    if (kayitBasarili == true)
                    {
                        var acilanCagri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == cagriIptal.CagriKayitNo);

                        dbMusteriCalisan.AcilanCagri.Remove(acilanCagri);
                        dbMusteriCalisan.SaveChanges();
                    }

                    return RedirectToAction("Index");

                }
                else
                    return View(_cagriIptal);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View();
            } 
        } 

        private void GelenCagriListYarat()
        {
            CagriBilgileri.cagriList.Clear();

            int temp = 0, countCagri = 0;
            int firmaID = 0;
            int _McID = -1;

            countCagri = dbMusteriCalisan.AcilanCagri.Count();

            for (temp = 0; temp < countCagri; temp++)
            {
                _McID = dbMusteriCalisan.AcilanCagri.ToList()[temp].McID;

                var _cagri = dbMusteriCalisan.AcilanCagri.ToList()[temp];
                var _musteriCalisani = dbMusteri.MusteriCalisani.SingleOrDefault(x => x.McID == _McID);
                var _musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == _musteriCalisani.MusteriID);
                var _sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == _musteri.ID);
                var _sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == _sozlesmeYapma.ID);
                var _firmaYonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(c => c.FyID == _sozlesmeYapma.FyID);

                if (_firmaYonetici != null)
                {
                    firmaID = _firmaYonetici.FirmaID;

                    if (Connection.parentID == firmaID) //firmaya ait cagrilar.
                    {
                        //TODO : temizlik gerekli sozlesmeler için.
                         
                        var cagri = new CagriBilgileri();

                        cagri.CagriNo = _cagri.CagriNo;
                        cagri.ID = _cagri.ID;
                        cagri.Adres = _musteri.Adres;
                        cagri.MusteriAdi = _musteri.MusteriAdi;
                        cagri.MusteriKodu = _musteri.MusteriKodu;
                        cagri.Sozlesme = _sozlesme.SozlesmeAdi;
                        cagri.CagriAcilisTarihi = _cagri.AcilisTarihi;

                        cagri.IlgiliKisi = _cagri.YetkiliKisi;
                        cagri.Telefon = _cagri.Gsm;
                        cagri.Email = _cagri.Email;
                        cagri.CihazTipi = _cagri.CihazTipi;
                        cagri.Marka = _cagri.Marka;
                        cagri.Model = _cagri.Model;
                        cagri.SeriNo = _cagri.SeriNo;
                        cagri.BarkodNo = _cagri.BarkodNo;
                        cagri.Aciklama = _cagri.Aciklama;
                        cagri.CagriDetayi = _cagri.CagriDetayi;
                        cagri.SarfMalzemeTalebi = _cagri.SarfMalzemeTalebi;
                        cagri.CreateUserID = _musteri.ID;

                        if(_cagri.IslemGorduMu==false)
                            cagri.IslemGorduMu = 0;
                        else
                            cagri.IslemGorduMu = 1;
                        
                        cagri.Durum = "Gelen Çağrı";

                        CagriBilgileri.cagriList.Add(cagri);

                    }
                }
            }

            CagriBilgileri.cagriList = CagriBilgileri.cagriList.OrderBy(x => x.CagriAcilisTarihi).ToList();
        }

        private void CalisanListYarat()
        {
            CalisanBilgileri.firmaCalisanList.Clear();

            int temp, count = 0;

            count = dbFirmaYonetici.FirmaCalisani.Count();

            for (temp = 0; temp < count; temp++)
            {
                if (Connection.parentID == dbFirmaYonetici.FirmaCalisani.ToList()[temp].FirmaID)
                {
                    int id = dbFirmaYonetici.FirmaCalisani.ToList()[temp].FcID;

                    var _firmaCalisanList = new CalisanBilgileri(); 
                    var _firmaCalisan = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(x=>x.FcID == id);

                    _firmaCalisanList.FcID = _firmaCalisan.FcID;
                    _firmaCalisanList.CalisanAdSoyad = _firmaCalisan.Ad +" "+_firmaCalisan.Soyad;
                    CalisanBilgileri.firmaCalisanList.Add(_firmaCalisanList);
                }
            }
            CalisanBilgileri.firmaCalisanList = CalisanBilgileri.firmaCalisanList.OrderByDescending(x => x.Ad).ToList();
        }  
    }
}
