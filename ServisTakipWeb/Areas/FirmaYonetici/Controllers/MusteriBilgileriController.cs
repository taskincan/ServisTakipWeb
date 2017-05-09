using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.FirmaYonetici.Models;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.FirmaYonetici.Controllers
{
    public class MusteriBilgileriController : BaseController
    {
        //
        // GET: /FirmaYonetici/MusteriBilgileri/

        public ActionResult Index()
        {
            MusteriListTemizle();
            MusteriListYarat();

            Connection.sayfaAdi = "Müşteri Bilgileri";

            return View(MusteriBilgileri.musteriList);
        }

        private void MusteriListYarat()
        {
            MusteriBilgileri.musteriList.Clear();

            int temp = 0, temp2 = 0, passLength = 0, countSozlesme = 0, countFirmaYonetici = 0, countMusteri = 0;
            int firmaID = 0;
            int _FyId = -1, _MId = -1, _SozlesmeId = -1;

            countSozlesme = dbFirmaYonetici.SozlesmeYapma.Count(); 

            for (temp = 0; temp < countSozlesme; temp++)
            {
                _FyId = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].FyID;

                var firmaYonetici = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(c => c.FyID == _FyId);

                if (firmaYonetici != null)
                {
                    firmaID = firmaYonetici.FirmaID;

                    if (Connection.parentID == firmaID) //sozlesmeyapma tablosunda firmaya ait anlaşmalar.
                    {
                        var _sozlesmeYapmaList = new SozlesmeYapma();
                        var _sozlesmeList = new Sozlesme();
                        var _musteriList = new MusteriBilgileri();

                        _SozlesmeId = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].SozlesmeID;
                        _MId = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].MID;

                        var musteri = dbFirmaYonetici.Musteri.SingleOrDefault(c => c.ID == _MId);
                        var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == _SozlesmeId);

                        _sozlesmeYapmaList.SozlesmeID = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].SozlesmeID;
                        _sozlesmeYapmaList.MID = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].MID;
                        _sozlesmeYapmaList.FyID = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].FyID;

                        _sozlesmeList.AnlasmaUcreti = sozlesme.AnlasmaUcreti;
                        _sozlesmeList.BaslangicTarih = sozlesme.BaslangicTarih;
                        _sozlesmeList.BitisTarih = sozlesme.BitisTarih;
                        _sozlesmeList.SlaSuresi = sozlesme.SlaSuresi;
                        _sozlesmeList.SozlesmeAdi = sozlesme.SozlesmeAdi;
                        _sozlesmeList.SozlesmeID = sozlesme.ID; 

                        _musteriList.Adres = musteri.Adres;
                        _musteriList.CreateDate = musteri.CreateDate;
                        _musteriList.Email = musteri.Email;
                        _musteriList.ID = musteri.ID;
                        _musteriList.MusteriAdi = musteri.MusteriAdi;
                        _musteriList.MusteriKodu = musteri.MusteriKodu;
                        _musteriList.Password = musteri.Password;
                        _musteriList.Tel1 = musteri.MusteriTel;
                        _musteriList.Tel2 = musteri.MusteriTel2;
                        _musteriList.VergiDairesi = musteri.VergiDairesi;
                        _musteriList.VergiNumarası = musteri.VergiNumarasi;
                        _musteriList.YetkiliKisi = musteri.YetkiliKisi;

                        passLength = (musteri.Password).Length;

                        for (temp2 = 0; temp2 < passLength; temp2++)
                        {
                            _musteriList.YoneteciPassword2 += "*";
                        }
                        _musteriList.CreateDate = dbFirma.FirmaYonetici.ToList()[temp].CreateDate;

                        MusteriBilgileri.musteriList.Add(_musteriList);

                    }
                }
            }
        }

        /*var _musteriList = new MusteriBilgileri();

                        _musteriList.Adres = dbFirmaYonetici.Musteri.ToList()[temp].Adres;
                        _musteriList.CreateDate=dbFirmaYonetici.Musteri.ToList()[temp].CreateDate;
                        _musteriList.Email = dbFirmaYonetici.Musteri.ToList()[temp].Email;
                        _musteriList.ID = dbFirmaYonetici.Musteri.ToList()[temp].ID;
                        _musteriList.MusteriAdi = dbFirmaYonetici.Musteri.ToList()[temp].MusteriAdi;
                        _musteriList.MusteriKodu = dbFirmaYonetici.Musteri.ToList()[temp].MusteriKodu;
                        _musteriList.Password = dbFirmaYonetici.Musteri.ToList()[temp].Password;
                        _musteriList.Tel1 = dbFirmaYonetici.Musteri.ToList()[temp].MusteriTel;
                        _musteriList.Tel2 = dbFirmaYonetici.Musteri.ToList()[temp].MusteriTel2;
                        _musteriList.VergiDairesi = dbFirmaYonetici.Musteri.ToList()[temp].VergiDairesi;
                        _musteriList.VergiNumarası = dbFirmaYonetici.Musteri.ToList()[temp].VergiNumarasi;
                        _musteriList.YetkiliKisi = dbFirmaYonetici.Musteri.ToList()[temp].YetkiliKisi; 
                     

                        passLength = (dbFirmaYonetici.Musteri.ToList()[temp].Password).Length;

                        for (int temp2 = 0; temp2 < passLength; temp2++)
                        {
                            _musteriList.YoneteciPassword2 += "*";
                        }

                        _musteriList.CreateDate = dbFirma.FirmaYonetici.ToList()[temp].CreateDate;

                        MusteriBilgileri.musteriList.Add(_musteriList);*/

        private void MusteriListTemizle()
        {
            MusteriBilgileri.musteriList.Clear();
        }

    }
}
