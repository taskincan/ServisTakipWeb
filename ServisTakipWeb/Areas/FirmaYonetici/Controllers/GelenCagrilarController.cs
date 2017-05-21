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
            CagriListYarat();

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

        private void CagriListYarat()
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
