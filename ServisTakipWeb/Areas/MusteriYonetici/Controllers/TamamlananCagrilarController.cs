using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Areas.MusteriYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.MusteriYonetici.Controllers
{
    public class TamamlananCagrilarController : BaseController
    {
        //
        // GET: /MusteriYonetici/TamamlananCagrilar/

        public ActionResult Index()
        {
            TamamlananCagriListYarat();

            Connection.sayfaAdi = "Tamamlanan Çağrılar";

            return View(CagriTamamlamaBilgileri.cagriTamamlamaMusteriList);
        }

        public ActionResult Goruntule(int _cagriNo = -1)
        {
            var tamamlananCagri = CagriTamamlamaBilgileri.cagriTamamlamaMusteriList.SingleOrDefault(x => x.CagriKayitNo == _cagriNo);

            if (tamamlananCagri == null)
                return View("Index");
            else
                return View(tamamlananCagri);
        }

        public ActionResult Anket(int _cagriNo = -1)
        {
            var anket = new AnketSorulari();

            anket.CagriNo = _cagriNo;

            return View(anket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anket(AnketSorulari _anket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var anket = new Context.Anket();
                    var anketYapma = new Context.AnketYapma();
                    var cagri = dbFirmaYonetici.TamamlananCagrilar.SingleOrDefault(x=>x.CagriKayitNo==_anket.CagriNo);

                    bool kayit = false;

                    anket.Soru1 = _anket.Soru1.ToString();
                    anket.Soru2 = _anket.Soru2.ToString();
                    anket.Soru3 = _anket.Soru3.ToString();
                    anket.Soru4 = _anket.Soru4.ToString();
                    anket.Soru5 = _anket.Soru5.ToString();
                    anket.TamamlananCagriID = cagri.TamamlananID;
                    anket.MusteriGorus = _anket.MusteriGorusu;

                    dbMusteriYonetici.Anket.Add(anket);
                    dbMusteriYonetici.SaveChanges();
                    kayit = true;
                    
                    if(kayit==true)
                    {
                        var kayitliAnket = dbMusteriYonetici.Anket.SingleOrDefault(x=>x.TamamlananCagriID == cagri.TamamlananID);

                        anketYapma.AnketID = kayitliAnket.ID;
                        anketYapma.TamamlananCagriID = kayitliAnket.TamamlananCagriID;
                        anketYapma.MyID = Connection.ID;
                        anketYapma.CreateDate = DateTime.Now;

                        kayit = false;
                        dbMusteriYonetici.AnketYapma.Add(anketYapma);
                        dbMusteriYonetici.SaveChanges();
                        kayit = true;

                        if(kayit==true)
                        { 
                            cagri.AnketYapildiMi = true;

                            dbFirmaYonetici.Entry(cagri).State = EntityState.Modified;
                            dbFirmaYonetici.SaveChanges();
                            ModelState.Clear();
                        }

                    }

                    return RedirectToAction("Index");
                }
                else
                    return View(_anket);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View(_anket);
            }

            _anket.AnketOrtalamaPuani = Convert.ToDouble(_anket.Soru1 + _anket.Soru2 + _anket.Soru3 + _anket.Soru4 + _anket.Soru5) / 5;

             
            return View();
        }


        private void TamamlananCagriListYarat()
        {
            CagriTamamlamaBilgileri.cagriTamamlamaMusteriList.Clear();

            int temp = 0,temp2=0,countCihaz=0, countCagri = 0, soru1 = 0, soru2 = 0, soru3 = 0, soru4 = 0, soru5 = 0; 

            var tamamlanancagriList = dbFirmaYonetici.TamamlananCagrilar.Where(x => x.MID == Connection.parentID);

            countCagri = tamamlanancagriList.Count();
             
            for (temp = 0; temp < countCagri; temp++)
            {
                var _cagri = tamamlanancagriList.ToList()[temp];
                var _musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == _cagri.MID);
                
                if(_cagri.AnketYapildiMi==true)
                { 
                    var _anketYapma = dbMusteriYonetici.AnketYapma.SingleOrDefault(x=>x.TamamlananCagriID==_cagri.TamamlananID); 
                    var _anket = dbMusteriYonetici.Anket.SingleOrDefault(x => x.ID == _anketYapma.AnketID);
                    
                    soru1= Convert.ToInt32(_anket.Soru1);
                    soru2 = Convert.ToInt32(_anket.Soru2);
                    soru3 = Convert.ToInt32(_anket.Soru3);
                    soru4 = Convert.ToInt32(_anket.Soru4);
                    soru5 = Convert.ToInt32(_anket.Soru5);
                }
               

                //TODO : temizlik gerekli sozlesmeler için.

                var tamamlananCagri = new CagriTamamlamaBilgileri();

                tamamlananCagri.TamamlananID = _cagri.TamamlananID;

                if (_cagri.TamamlayanYoneticiID == -1) // calisan tamamlamis
                {
                    tamamlananCagri.TamamlayanCalisanID = Convert.ToInt32(_cagri.TamamlayanCalisanID);

                    var _firmaCalisan = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(c => c.FcID == tamamlananCagri.TamamlayanCalisanID);

                    tamamlananCagri.TamamlayanKisi = _firmaCalisan.Ad + " " + _firmaCalisan.Soyad;
                }
                else // yonetici tamamlamis
                {
                    var _firmaYonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(c => c.FyID == _cagri.TamamlayanYoneticiID);

                    tamamlananCagri.TamamlayanCalisanID = Convert.ToInt32(_cagri.TamamlayanYoneticiID);
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
                tamamlananCagri.AnketYapildiMi = _cagri.AnketYapildiMi;

                if (_cagri.AnketYapildiMi == true)
                {
                    double anketOrt = 0;

                    anketOrt = Convert.ToDouble(soru1 + soru2 + soru3 + soru4 + soru5) / 5; 
                    
                    tamamlananCagri.AnketYapildiMiTablo = anketOrt.ToString();  
                }
                else
                    tamamlananCagri.AnketYapildiMiTablo = "Yapılmamış";

                var cihazBilgileri = dbFirmaYonetici.CihazBilgileri.Where(x => x.CagriNo == _cagri.CagriKayitNo);
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

                CagriTamamlamaBilgileri.cagriTamamlamaMusteriList.Add(tamamlananCagri); 
            }
            CagriTamamlamaBilgileri.cagriTamamlamaMusteriList = CagriTamamlamaBilgileri.cagriTamamlamaMusteriList.OrderByDescending(x => x.HizmetBitisTarihi).ToList();

        }

    }
}
