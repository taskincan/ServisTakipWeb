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
    public class BekleyenCagrilarController : BaseController
    {
        //
        // GET: /FirmaYonetici/BekleyenCagrilar/

        public ActionResult Index()
        {
            AtanmisCagrilarListYarat();
            GelenCagriListYarat();

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

        public ActionResult Atama(int _cagriNo = -1)
        {
            var atananCagri = dbFirmaYonetici.AtananCagrilar.SingleOrDefault(x=>x.CagriNo == _cagriNo);
            var _cagri = CagriBilgileri.cagriList.SingleOrDefault(x => x.CagriNo == _cagriNo);
            var cagriAtama = new CagriAtamaBilgileri();
             
            CalisanListYarat();

            CagriAtamaBilgileri.calisanList = CalisanBilgileri.firmaCalisanList.ToList();

            cagriAtama.AtananID = 1;
            cagriAtama.ID = atananCagri.ID;
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

                    cagriAtama.ID = _cagriAtama.ID;
                    cagriAtama.Aciliyet = _cagriAtama.AcilMi;
                    cagriAtama.AtananID = _cagriAtama.AtananID;
                    cagriAtama.AtayanID = Connection.ID;
                    cagriAtama.CagriNo = _cagriAtama.CagriNo;
                    cagriAtama.CreateDate = DateTime.Now;
                    cagriAtama.VarisTarih = _cagriAtama.VarisTarih;
                    cagriAtama.YoneticiNotu = _cagriAtama.Not;

                    bool kayitBasarili = false;
                     
                    dbFirmaYonetici.Entry(cagriAtama).State = EntityState.Modified;
                    dbFirmaYonetici.SaveChanges();
                    ModelState.Clear();

                    kayitBasarili = true;

                    if (kayitBasarili == true)
                    {
                        var acilanCagri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == _cagriAtama.CagriNo);
                         
                        acilanCagri.IslemGorduMu = true; 

                        dbMusteriCalisan.Entry(acilanCagri).State = EntityState.Modified;
                        dbMusteriCalisan.SaveChanges();

                        var bekleyenCagri = dbFirmaYonetici.BekleyenCagrilar.SingleOrDefault(x => x.CagriNo == _cagriAtama.CagriNo);
                        
                        dbFirmaYonetici.BekleyenCagrilar.Remove(bekleyenCagri);
                        dbFirmaYonetici.SaveChanges();
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
                    var _firmaCalisan = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(x => x.FcID == id);

                    _firmaCalisanList.FcID = _firmaCalisan.FcID;
                    _firmaCalisanList.CalisanAdSoyad = _firmaCalisan.Ad + " " + _firmaCalisan.Soyad;
                    CalisanBilgileri.firmaCalisanList.Add(_firmaCalisanList);
                }
            }
            CalisanBilgileri.firmaCalisanList = CalisanBilgileri.firmaCalisanList.OrderByDescending(x => x.Ad).ToList();
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

                        if (_cagri.IslemGorduMu == false)
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
    }
}
