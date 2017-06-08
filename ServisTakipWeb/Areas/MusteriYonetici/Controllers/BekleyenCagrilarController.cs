using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.MusteriYonetici.Controllers
{
    public class BekleyenCagrilarController : BaseController
    {
        //
        // GET: /MusteriYonetici/BekleyenCagrilar/

        public ActionResult Index()
        {
            AcilanCagriListYarat();
            BekleyenCagriListYarat();

            return View(AtanmisCagriBilgileri.cagriAtanmisList);
        }

        public ActionResult Goruntule(int _cagriNo = -1)
        {
            var cagri = CagriBilgileri.cagriList.SingleOrDefault(x => x.CagriNo == _cagriNo);

            if (cagri == null)
                return View("Index");
            else
                return View(cagri);
        }

        private void AcilanCagriListYarat()
        {
            CagriBilgileri.cagriList.Clear();

            int temp = 0, countCagri = 0;

            var musteriCalisan = dbMusteri.MusteriCalisani.SingleOrDefault(x => x.MusteriID == Connection.parentID);
            var cagrii = dbMusteriCalisan.AcilanCagri.Where(x => x.McID == musteriCalisan.McID);

            countCagri = cagrii.Count();

            for (temp = 0; temp < countCagri; temp++)
            {
                var _cagri = cagrii.ToList()[temp];
                var _musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == musteriCalisan.MusteriID);
                var _sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == _musteri.ID);
                var _sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == _sozlesmeYapma.ID);

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
            CagriBilgileri.cagriList = CagriBilgileri.cagriList.OrderBy(x => x.CagriAcilisTarihi).ToList();
        }

        private void BekleyenCagriListYarat()
        {
            AtanmisCagriBilgileri.cagriAtanmisList.Clear();

            var musteriYonetici = dbMusteri.MusteriYonetici.SingleOrDefault(x => x.MyID == Connection.ID);
            var cagrilar = CagriBilgileri.cagriList.Where(x => x.MusteriKodu == musteriYonetici.Musteri.MusteriKodu);
            var musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == musteriYonetici.MusteriID);

            

            int count = 0, temp = 0, cagriNo = 0;

            count = cagrilar.Count();

            for (temp = 0; temp < count; temp++)
            {
                var atanmisCagrilar = new AtanmisCagriBilgileri();
                 
                cagriNo = cagrilar.ToList()[temp].CagriNo;

                var atananCagri = dbFirmaYonetici.AtananCagrilar.SingleOrDefault(x => x.CagriNo == cagriNo);
                
                if(atananCagri != null)
                {
                    var cagriBilgileri = dbMusteriCalisan.AcilanCagri.SingleOrDefault(x => x.CagriNo == cagriNo);
                    var bekleyenCagri = dbFirmaYonetici.BekleyenCagrilar.SingleOrDefault(x => x.CagriNo == cagriNo);
                    var firmaCalisani = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(x=>x.FcID== atananCagri.AtananID);
                    
                    atanmisCagrilar.Durum = "Bekleyen Çağrı";
                    atanmisCagrilar.BeklemeyeAlmaNedeni = bekleyenCagri.BeklemeNedeni;

                    atanmisCagrilar.AtananAdiSoyadi = firmaCalisani.Ad + " " + firmaCalisani.Soyad;

                    atanmisCagrilar.CagriNo = cagriNo; 

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
                
            }

            AtanmisCagriBilgileri.cagriAtanmisList = AtanmisCagriBilgileri.cagriAtanmisList.OrderBy(x => x.CreateDate).ToList();
        }

    }
}
