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

        public ActionResult Musteri(int id = 0)
        {
            int temp=0, passLength = 0, _createUserID = -1;

            var _musteri = new MusteriBilgileri();  
            
            _createUserID =  CreateUserIDGetirByMusteriID(id);

            var musteri = dbFirmaYonetici.Musteri.SingleOrDefault(x => x.ID == id);
            
            if (musteri != null)
            {
                _musteri.Adres = musteri.Adres;
                _musteri.CreateDate = musteri.CreateDate;
                _musteri.Email = musteri.Email;
                _musteri.ID = musteri.ID;
                _musteri.MusteriAdi = musteri.MusteriAdi;
                _musteri.MusteriKodu = musteri.MusteriKodu;
                _musteri.Password = musteri.Password;
                _musteri.Tel1 = musteri.MusteriTel;
                _musteri.Tel2 = musteri.MusteriTel2;
                _musteri.VergiDairesi = musteri.VergiDairesi;
                _musteri.VergiNumarasi = musteri.VergiNumarasi;
                _musteri.YetkiliKisi = musteri.YetkiliKisi;
                _musteri.CreateUserID = _createUserID;

                passLength = (musteri.Password).Length;

                for (temp = 0; temp < passLength; temp++)
                {
                    _musteri.YoneteciPassword2 += "*";
                }
            }
              
             if (_musteri == null)
                return RedirectToAction("Index");
            
            return View(_musteri);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Musteri(MusteriBilgileri _musteriBilgileri)
        {
            if (ModelState.IsValid)
            {
                MusteriBilgileri musteri = MusteriBilgileri.musteriList.SingleOrDefault(x => x.ID == _musteriBilgileri.ID);

                //var updatedMusteri = dbFirmaYonetici.Musteri.SingleOrDefault(x => x.ID == _musteriBilgileri.ID);

                musteri.ID = _musteriBilgileri.ID;
                musteri.Adres = _musteriBilgileri.Adres;
                musteri.Email = _musteriBilgileri.Email;
                musteri.MusteriAdi = _musteriBilgileri.MusteriAdi;
                musteri.MusteriKodu = _musteriBilgileri.MusteriKodu;
                musteri.Password = _musteriBilgileri.Password;
                musteri.Tel1 = _musteriBilgileri.Tel1;
                musteri.Tel2 = _musteriBilgileri.Tel2;
                musteri.VergiDairesi = _musteriBilgileri.VergiDairesi;
                musteri.VergiNumarasi = _musteriBilgileri.VergiNumarasi;
                musteri.YetkiliKisi = _musteriBilgileri.YetkiliKisi;
                musteri.CreateDate = DateTime.Now;

                /*dbFirmaYonetici.Entry(updatedMusteri).State = EntityState.Modified;
                dbFirmaYonetici.SaveChanges();
                ModelState.Clear();*/

                //return RedirectToAction("Sozlesme", "MusteriBilgileri", _musteriBilgileri);
                return RedirectToAction("Sozlesme", "MusteriBilgileri", new { id = _musteriBilgileri.ID });
            }
            return View(_musteriBilgileri);
        }


        public ActionResult Sozlesme(int id=-1)
        {
            MusteriBilgileri musteri = MusteriBilgileri.musteriList.SingleOrDefault(x => x.ID == id);

            SozlesmeYapma sozlesmeYapma = SozlesmeYapma.sozlesmeYapmaList.SingleOrDefault(x => x.MID == id);
            //SozlesmeBilgileri sozlesme2 = SozlesmeBilgileri.sozlesmeList.SingleOrDefault(x => x.SozlesmeID == 2);

            //var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == id);
            var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == sozlesmeYapma.SozlesmeID);

            var _sozlesme = SozlesmeBilgileri.sozlesmeList.SingleOrDefault(x => x.SozlesmeID == sozlesme.ID);
             
            if (sozlesme != null)
            { 
                _sozlesme.SozlesmeID = sozlesme.ID;
                _sozlesme.SozlesmeAdi = sozlesme.SozlesmeAdi;
                
                _sozlesme.AnlasmaUcreti = sozlesme.AnlasmaUcreti.ToString();
                _sozlesme.SlaSuresi = sozlesme.SlaSuresi;
               _sozlesme.BaslangicTarih = sozlesme.BaslangicTarih;
                 
                _sozlesme.BitisTarih = sozlesme.BitisTarih;
                _sozlesme.ParcaDahilMi = sozlesme.ParcaDahilMi;

                //Burayı dusun.
            }
            else
                return RedirectToAction("Index");
             
            return View(_sozlesme);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sozlesme(SozlesmeBilgileri _sozlesmeBilgileri)
        {
             
            if (ModelState.IsValid)
            {
                var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == _sozlesmeBilgileri.SozlesmeID);
                var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == sozlesmeYapma.ID);

                if (_sozlesmeBilgileri != null)
                {
                    sozlesme.ID = _sozlesmeBilgileri.SozlesmeID;
                    sozlesme.SozlesmeAdi = _sozlesmeBilgileri.SozlesmeAdi;
                    sozlesme.SlaSuresi = _sozlesmeBilgileri.SlaSuresi;
                    sozlesme.AnlasmaUcreti = Convert.ToDecimal(_sozlesmeBilgileri.AnlasmaUcreti);
                    sozlesme.BaslangicTarih = _sozlesmeBilgileri.BaslangicTarih;
                    sozlesme.BitisTarih = _sozlesmeBilgileri.BitisTarih;
                    sozlesme.ParcaDahilMi = _sozlesmeBilgileri.ParcaDahilMi;
                }

                dbFirmaYonetici.Entry(sozlesme).State = EntityState.Modified;
                dbFirmaYonetici.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(_sozlesmeBilgileri);



            //TODO: SozlesmeBilgileri modelini almıyor. MusteriBilgilerini alıyor null ama yinede alıyor. !!!
            //TODO: Onemli
            return RedirectToAction("Index");
        }

        private void MusteriListYarat()
        {
            MusteriBilgileri.musteriList.Clear();

            int temp = 0, temp2 = 0, passLength = 0, countSozlesme = 0;
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
                        //TODO : temizlik gerekli sozlesmeler için.

                        var _sozlesmeYapmaList = new SozlesmeYapma();
                        var _sozlesmeList = new SozlesmeBilgileri();
                        var _musteriList = new MusteriBilgileri();

                        _SozlesmeId = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].SozlesmeID;
                        _MId = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].MID;

                        var musteri = dbFirmaYonetici.Musteri.SingleOrDefault(c => c.ID == _MId);
                        var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == _SozlesmeId);

                        _sozlesmeYapmaList.SozlesmeID = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].SozlesmeID;
                        _sozlesmeYapmaList.MID = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].MID;
                        _sozlesmeYapmaList.FyID = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].FyID;

                        _sozlesmeList.AnlasmaUcreti = sozlesme.AnlasmaUcreti.ToString();
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
                        _musteriList.VergiNumarasi = musteri.VergiNumarasi;
                        _musteriList.YetkiliKisi = musteri.YetkiliKisi;
                        _musteriList.CreateUserID = dbFirmaYonetici.SozlesmeYapma.ToList()[temp].FyID;

                        passLength = (musteri.Password).Length;

                        for (temp2 = 0; temp2 < passLength; temp2++)
                        {
                            _musteriList.YoneteciPassword2 += "*";
                        }

                        MusteriBilgileri.musteriList.Add(_musteriList);
                        SozlesmeBilgileri.sozlesmeList.Add(_sozlesmeList);
                        SozlesmeYapma.sozlesmeYapmaList.Add(_sozlesmeYapmaList);

                    }
                }
            }
        } 

        private void MusteriListTemizle()
        {
            MusteriBilgileri.musteriList.Clear();
            SozlesmeBilgileri.sozlesmeList.Clear();
            SozlesmeYapma.sozlesmeYapmaList.Clear();
        }

        private int CreateUserIDGetirByMusteriID(int _MID)
        {
            var sozlesme = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(c => c.MID == _MID);

            if (sozlesme != null)
                return sozlesme.FyID;
            else
                return -1;
        } 
    }
}
