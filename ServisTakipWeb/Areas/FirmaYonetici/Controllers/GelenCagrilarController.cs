using System;
using System.Collections.Generic;
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
            var cagri = CagriBilgileri.cagriList.SingleOrDefault(x=>x.CagriNo == _cagriNo);
            
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
            cagriTamamla.Email = musteri.Email;
            cagriTamamla.FormNo = 0;
            cagriTamamla.MusteriAdi = cagri.MusteriAdi;
            cagriTamamla.YetkiliKisi = cagri.IlgiliKisi;
            cagriTamamla.Telefon = cagri.Telefon;
            cagriTamamla.Adres = cagri.Adres;
            cagriTamamla.VergiDairesi = musteri.VergiDairesi;
            cagriTamamla.VergiNumarasi = musteri.VergiNumarasi;
            cagriTamamla.MusteriKodu = cagri.MusteriKodu;
            cagriTamamla.BildirilenAriza = cagri.CagriDetayi;
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
                var _musteri = dbMusteri.Musteri.SingleOrDefault(x=>x.ID==_musteriCalisani.MusteriID);
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
                        cagri.CreateUserID = _McID;
                        cagri.IslemGorduMu = false;
                        cagri.Durum = "Gelen Çağrı";
                              
                        CagriBilgileri.cagriList.Add(cagri);

                    }
                }
            }
        }

    }
}
