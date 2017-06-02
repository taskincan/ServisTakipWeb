using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Areas.MusteriYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class TamamlananCagrilarController : BaseController
    {
        //
        // GET: /FirmaYonetici/TamamlananCagrilar/

        public ActionResult Index()
        {
            TamamlananCagriListYarat();

            Connection.sayfaAdi = "Tamamlanan Çağrılar";

            return View(CagriTamamlamaBilgileri.cagriTamamlamaList);
        }

        public ActionResult Goruntule(int _cagriNo = -1)
        {
            var tamamlananCagri = CagriTamamlamaBilgileri.cagriTamamlamaList.SingleOrDefault(x => x.CagriKayitNo == _cagriNo);

            if (tamamlananCagri == null)
                return View("Index");
            else
                return View(tamamlananCagri);
        }

        public ActionResult AnketSonuclari(int _cagriNo = -1)
        {
            var anket = new AnketSorulari();
            var cagri = CagriTamamlamaBilgileri.cagriTamamlamaList.SingleOrDefault(x => x.CagriKayitNo == _cagriNo);
            var anketSonuclari = dbMusteriYonetici.Anket.SingleOrDefault(x => x.TamamlananCagriID == cagri.TamamlananID);
            var anketYapma = anketSonuclari.AnketYapma.SingleOrDefault(x => x.TamamlananCagriID == cagri.TamamlananID);
            var musteriYonetici = dbMusteri.MusteriYonetici.SingleOrDefault(x => x.MyID == anketYapma.MyID);
            
            if (anketSonuclari != null)
            {
                anket.Soru1 = anketSonuclari.Soru1;
                anket.Soru2 = anketSonuclari.Soru2;
                anket.Soru3 = anketSonuclari.Soru3;
                anket.Soru4 = anketSonuclari.Soru4;
                anket.Soru5 = anketSonuclari.Soru5;
                anket.MusteriGorusu = anketSonuclari.MusteriGorus;
                anket.MyAdiSoyadi = musteriYonetici.Ad + " " + musteriYonetici.Soyad;
                anket.CagriNo = cagri.CagriKayitNo;

                return View(anket);
            }
            else
                return View("Index");


        }


        private void TamamlananCagriListYarat()
        {
            CagriTamamlamaBilgileri.cagriTamamlamaList.Clear();

            int temp = 0, countCagri = 0, soru1 = 0, soru2 = 0, soru3 = 0, soru4 = 0, soru5 = 0; 
            int firmaID = 0;
            int _MID = -1;

            countCagri = dbFirmaYonetici.TamamlananCagrilar.Count();

            for (temp = 0; temp < countCagri; temp++)
            {
                _MID = dbFirmaYonetici.TamamlananCagrilar.ToList()[temp].MID;

                var _cagri = dbFirmaYonetici.TamamlananCagrilar.ToList()[temp];
                var _musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == _MID);
                var _sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == _musteri.ID);
                var _sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == _sozlesmeYapma.ID);
                var _firmaYonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(c => c.FyID == _sozlesmeYapma.FyID);

                if (_firmaYonetici != null)
                {
                    firmaID = _firmaYonetici.FirmaID;

                    if (Connection.parentID == firmaID) //firmaya ait cagrilar.
                    {
                        //TODO : temizlik gerekli sozlesmeler için.

                        var tamamlananCagri = new CagriTamamlamaBilgileri();

                        tamamlananCagri.TamamlananID = _cagri.TamamlananID;

                        if (_cagri.AnketYapildiMi == true)
                        {
                            var _anketYapma = dbMusteriYonetici.AnketYapma.SingleOrDefault(x => x.TamamlananCagriID == _cagri.TamamlananID);
                            var _anket = dbMusteriYonetici.Anket.SingleOrDefault(x => x.ID == _anketYapma.AnketID);

                            soru1 = Convert.ToInt32(_anket.Soru1);
                            soru2 = Convert.ToInt32(_anket.Soru2);
                            soru3 = Convert.ToInt32(_anket.Soru3);
                            soru4 = Convert.ToInt32(_anket.Soru4);
                            soru5 = Convert.ToInt32(_anket.Soru5);
                        }


                        if (_cagri.TamamlayanYoneticiID == -1)
                        {
                            tamamlananCagri.TamamlayanCalisanID = Convert.ToInt32(_cagri.TamamlayanCalisanID);

                            var _firmaCalisan = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(c => c.FcID == tamamlananCagri.TamamlayanCalisanID);

                            tamamlananCagri.TamamlayanKisi = _firmaCalisan.Ad + " " + _firmaCalisan.Soyad;
                        }
                        else
                        {
                            tamamlananCagri.TamamlayanYoneticiID = Convert.ToInt32(_cagri.TamamlayanYoneticiID); //Firma Yonetici Panelindeyiz.

                            _firmaYonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(c => c.FyID == tamamlananCagri.TamamlayanYoneticiID);

                            tamamlananCagri.TamamlayanKisi = _firmaYonetici.Ad + " " + _firmaYonetici.Soyad;
                        }

                        tamamlananCagri.FormNo = _cagri.FormNo;
                        tamamlananCagri.MusteriID = _musteri.ID;
                        tamamlananCagri.Sonuc = _cagri.Sonuc;
                        tamamlananCagri.Durum = "Tamamlandı"; 
                        tamamlananCagri.Email = _musteri.Email;
                        tamamlananCagri.MusteriAdi = _musteri.MusteriAdi;
                        tamamlananCagri.YetkiliKisi = _cagri.YetkiliKisi;
                        tamamlananCagri.Telefon = _cagri.Gsm;
                        tamamlananCagri.Adres = _musteri.Adres;
                        tamamlananCagri.VergiDairesi = _musteri.VergiDairesi;
                        tamamlananCagri.VergiNumarasi = _musteri.VergiNumarasi;
                        tamamlananCagri.MusteriKodu = _musteri.MusteriKodu;
                        tamamlananCagri.BildirilenAriza = _cagri.BildirilenAriza;
                        tamamlananCagri.HizmetTipi = _cagri.HizmetTipi;
                        tamamlananCagri.CihazinHizmetDurumu = _cagri.CihazinHizmetDurumu;

                        tamamlananCagri.CagriBildirildigiTarih = _cagri.CagrininBildirigiTarih;
                        tamamlananCagri.HizmetBaslangicTarihi = _cagri.HizmetBaslangicTarihi;
                        tamamlananCagri.HizmetBitisTarihi = _cagri.HizmetBitisTarihi;
                        tamamlananCagri.CagriKayitNo = _cagri.CagriKayitNo;
                        tamamlananCagri.MesaiSaatiIcindeMi = _cagri.MesaiSaatiIcindeMi;
                        tamamlananCagri.YapilanIsinAciklamasi = _cagri.YapılanIsinAciklamasi;
                        tamamlananCagri.CreateDate = _cagri.CreateDate;

                        if (_cagri.AnketYapildiMi == true)
                        {
                            double anketOrt = 0;

                            anketOrt = Convert.ToDouble(soru1 + soru2 + soru3 + soru4 + soru5) / 5;

                            tamamlananCagri.AnketYapildiMiTablo = anketOrt.ToString();
                            tamamlananCagri.AnketYapildiMi = true;
                        }
                        else
                        {
                            tamamlananCagri.AnketYapildiMiTablo = "Yapılmamış";
                            tamamlananCagri.AnketYapildiMi = false;
                        }
                        /*cagriTamamla.Marka1 = "";
                        tamamlananCagri.Marka2 = "";
                        tamamlananCagri.Marka3 = "";
                        tamamlananCagri.Marka4 = "";
                        tamamlananCagri.Model1 = "";
                        tamamlananCagri.Model2 = "";
                        tamamlananCagri.Model3 = "";
                        tamamlananCagri.Model4 = "";
                        tamamlananCagri.SeriNo1 = "";
                        tamamlananCagri.SeriNo2 = "";
                        tamamlananCagri.SeriNo3 = "";
                        tamamlananCagri.SeriNo4 = "";*/

                        /*tamamlananCagri.ParcaNo1 = "";
                        tamamlananCagri.ParcaNo2 = "";
                        tamamlananCagri.ParcaNo3 = "";
                        tamamlananCagri.ParcaAdi1 = "";
                        tamamlananCagri.ParcaAdi2 = "";
                        tamamlananCagri.ParcaAdi3 = "";
                        tamamlananCagri.Miktar1 = 0;
                        tamamlananCagri.Miktar2 = 0;
                        tamamlananCagri.Miktar3 = 0;
                        tamamlananCagri.BirimFiyati1 = 0;
                        tamamlananCagri.BirimFiyati2 = 0;
                        tamamlananCagri.BirimFiyati3 = 0;

                        tamamlananCagri.AciklamaIscilik1 = "";
                        tamamlananCagri.AciklamaIscilik2 = "";
                        tamamlananCagri.AciklamaIscilik3 = "";
                        tamamlananCagri.Sure1 = 0;
                        tamamlananCagri.Sure2 = 0;
                        tamamlananCagri.Sure3 = 0;
                        tamamlananCagri.BirimFiyatiIscilik1 = 0;
                        tamamlananCagri.BirimFiyatiIscilik2 = 0;
                        tamamlananCagri.BirimFiyatiIscilik3 = 0;*/

                        CagriTamamlamaBilgileri.cagriTamamlamaList.Add(tamamlananCagri);
                    }
                }
            }
            CagriTamamlamaBilgileri.cagriTamamlamaList = CagriTamamlamaBilgileri.cagriTamamlamaList.OrderByDescending(x => x.HizmetBitisTarihi).ToList();
        }

    }
}
