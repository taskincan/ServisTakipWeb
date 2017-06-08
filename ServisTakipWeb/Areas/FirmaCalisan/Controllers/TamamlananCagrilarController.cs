using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaCalisan.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaCalisan.Controllers
{
    public class TamamlananCagrilarController : BaseController
    {
        //
        // GET: /FirmaCalisan/TamamlananCagrilar/

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

        private void TamamlananCagriListYarat()
        {
            CagriTamamlamaBilgileri.cagriTamamlamaList.Clear();

            int temp = 0,temp2=0,countCihaz=0, countCagri = 0;
            int firmaID = 0;
            int _MID = -1;

            var tamamlananCagriList = dbFirmaYonetici.TamamlananCagrilar.Where(x => x.TamamlayanCalisanID == Connection.ID);

            countCagri = tamamlananCagriList.Count();

            for (temp = 0; temp < countCagri; temp++)
            {

                //TODO : temizlik gerekli sozlesmeler için.

                var tamamlananCagri = new CagriTamamlamaBilgileri();
                var _tamamlananCagri = tamamlananCagriList.ToList()[temp];

                var _musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == _tamamlananCagri.MID);
                var _firmaYonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(c => c.FyID == Connection.parentID);

                //TODO : temizlik gerekli sozlesmeler için.

                tamamlananCagri.TamamlananID = _tamamlananCagri.TamamlananID;
                tamamlananCagri.TamamlayanCalisanID = Convert.ToInt32(_tamamlananCagri.TamamlayanCalisanID);


                tamamlananCagri.FormNo = _tamamlananCagri.FormNo;
                tamamlananCagri.MusteriID = _musteri.ID;
                tamamlananCagri.Sonuc = _tamamlananCagri.Sonuc;
                tamamlananCagri.Durum = "Tamamlandı";
                tamamlananCagri.Email = _musteri.Email;

                tamamlananCagri.MusteriAdi = _musteri.MusteriAdi;
                tamamlananCagri.YetkiliKisi = _tamamlananCagri.YetkiliKisi;
                tamamlananCagri.Telefon = _tamamlananCagri.Gsm;
                tamamlananCagri.Adres = _musteri.Adres;
                tamamlananCagri.VergiDairesi = _musteri.VergiDairesi;
                tamamlananCagri.VergiNumarasi = _musteri.VergiNumarasi;
                tamamlananCagri.MusteriKodu = _musteri.MusteriKodu;
                tamamlananCagri.BildirilenAriza = _tamamlananCagri.BildirilenAriza;
                tamamlananCagri.HizmetTipi = _tamamlananCagri.HizmetTipi;
                tamamlananCagri.CihazinHizmetDurumu = _tamamlananCagri.CihazinHizmetDurumu;

                tamamlananCagri.CagriBildirildigiTarih = _tamamlananCagri.CagrininBildirigiTarih;
                tamamlananCagri.HizmetBaslangicTarihi = _tamamlananCagri.HizmetBaslangicTarihi;
                tamamlananCagri.HizmetBitisTarihi = _tamamlananCagri.HizmetBitisTarihi;
                tamamlananCagri.CagriKayitNo = _tamamlananCagri.CagriKayitNo;
                tamamlananCagri.MesaiSaatiIcindeMi = _tamamlananCagri.MesaiSaatiIcindeMi;
                tamamlananCagri.YapilanIsinAciklamasi = _tamamlananCagri.YapılanIsinAciklamasi;
                tamamlananCagri.CreateDate = _tamamlananCagri.CreateDate;

                if (_tamamlananCagri.AnketYapildiMi == true)
                    tamamlananCagri.AnketYapildiMiTablo = "Yapılmış";
                else
                    tamamlananCagri.AnketYapildiMiTablo = "Yapılmamış";

                var cihazBilgileri = dbFirmaYonetici.CihazBilgileri.Where(x => x.CagriNo == _tamamlananCagri.CagriKayitNo);
                countCihaz = cihazBilgileri.Count();

                for (temp2 = 0; temp2 < countCihaz; temp2++)
                {
                    if (temp2 == 0)
                    {
                        tamamlananCagri.Marka1 = cihazBilgileri.ToList()[temp2].Marka;
                        tamamlananCagri.Model1 = cihazBilgileri.ToList()[temp2].Model;
                        tamamlananCagri.SeriNo1 = cihazBilgileri.ToList()[temp2].SeriNo;
                    }
                    else if (temp2 == 1)
                    {
                        tamamlananCagri.Marka2 = cihazBilgileri.ToList()[temp2].Marka;
                        tamamlananCagri.Model2 = cihazBilgileri.ToList()[temp2].Model;
                        tamamlananCagri.SeriNo2 = cihazBilgileri.ToList()[temp2].SeriNo;
                    }
                    else if (temp2 == 2)
                    {
                        tamamlananCagri.Marka3 = cihazBilgileri.ToList()[temp2].Marka;
                        tamamlananCagri.Model3 = cihazBilgileri.ToList()[temp2].Model;
                        tamamlananCagri.SeriNo3 = cihazBilgileri.ToList()[temp2].SeriNo;
                    }
                    else if (temp2 == 3)
                    {
                        tamamlananCagri.Marka4 = cihazBilgileri.ToList()[temp2].Marka;
                        tamamlananCagri.Model4 = cihazBilgileri.ToList()[temp2].Model;
                        tamamlananCagri.SeriNo4 = cihazBilgileri.ToList()[temp2].SeriNo;
                    }
                }

                //TODO: Ucretli iscilikleri yap

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
            CagriTamamlamaBilgileri.cagriTamamlamaList = CagriTamamlamaBilgileri.cagriTamamlamaList.OrderByDescending(x => x.HizmetBitisTarihi).ToList();
        }

    }
}
