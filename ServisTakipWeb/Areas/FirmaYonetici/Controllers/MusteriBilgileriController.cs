using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
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

        public ActionResult Musteri(int id = -1)
        {
            int temp = 0, passLength = 0, _createUserID = -1;

            var _musteri = new MusteriBilgileri();

            if (id == -1)
            {
                _musteri.MusteriKodu = "0";
                return View(_musteri);
            }
            else
            {
                _createUserID = CreateUserIDGetirByMusteriID(id);

                MusteriBilgileri musteri = MusteriBilgileri.musteriList.SingleOrDefault(x => x.ID == id);

                if (musteri != null)
                {
                    _musteri.Adres = musteri.Adres;
                    _musteri.CreateDate = musteri.CreateDate;
                    _musteri.Email = musteri.Email;
                    _musteri.ID = musteri.ID;
                    _musteri.MusteriAdi = musteri.MusteriAdi;
                    _musteri.MusteriKodu = musteri.MusteriKodu;
                    _musteri.Password = musteri.Password;
                    _musteri.Tel1 = musteri.Tel1;
                    _musteri.Tel2 = musteri.Tel2;
                    _musteri.VergiDairesi = musteri.VergiDairesi;
                    _musteri.VergiNumarasi = musteri.VergiNumarasi;
                    _musteri.YetkiliKisi = musteri.YetkiliKisi;
                    _musteri.CreateUserID = _createUserID;

                    passLength = (musteri.Password).Length;

                    for (temp = 0; temp < passLength; temp++)
                    {
                        _musteri.Password2 += "*";
                    }
                }

                if (_musteri == null)
                    return RedirectToAction("Index");

                return View(_musteri);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Musteri(MusteriBilgileri _musteriBilgileri)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MusteriBilgileri musteri = MusteriBilgileri.musteriList.SingleOrDefault(x => x.ID == _musteriBilgileri.ID);

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

                    return RedirectToAction("Sozlesme", "MusteriBilgileri", new { id = _musteriBilgileri.ID, id2 = "" });
                }
                return View(_musteriBilgileri);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View(_musteriBilgileri);
            }
        }


        public ActionResult Sozlesme(int id = -1, string kod = "-1")
        {
            SozlesmeBilgileri _sozlesme2 = new SozlesmeBilgileri();
            try
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

                    _sozlesme.AnlasmaUcreti = Convert.ToDouble(sozlesme.AnlasmaUcreti);
                    _sozlesme.SlaSuresi = sozlesme.SlaSuresi;
                    _sozlesme.BaslangicTarih = sozlesme.BaslangicTarih;

                    _sozlesme.BitisTarih = sozlesme.BitisTarih;
                    _sozlesme.ParcaDahilMi = sozlesme.ParcaDahilMi;
                    _sozlesme.MusteriID = musteri.ID;

                    var pdf = GetFileList(musteri.ID);

                    _sozlesme.FileContent = pdf.ToList()[0].FileContent;
                    _sozlesme.FileName = pdf.ToList()[0].FileName;
                    _sozlesme.files = pdf.ToList()[0].files;
                    _sozlesme.Idpdf = pdf.ToList()[0].Idpdf;
                       
                    //Burayı dusun.
                }
                else
                    return RedirectToAction("Index");

                return View(_sozlesme);
                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View(_sozlesme2);
            }

        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            List<SozlesmeBilgileri> ObjFiles = GetFileList(id);

            var FileById = (from FC in ObjFiles
                            where FC.MusteriID.Equals(id)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName);

        }

        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<SozlesmeBilgileri> DetList = GetFileList(1);

            var yeniList = DetList.Where(x => x.MusteriID == 1);

            return PartialView("FileDetails", DetList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sozlesme(SozlesmeBilgileri _sozlesmeBilgileri)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_sozlesmeBilgileri.AnlasmaUcreti < 0 || _sozlesmeBilgileri.SlaSuresi <= 0)
                    {
                        ModelState.AddModelError("", "Anlaşma Ücretini veya SLA Süresini kontrol ediniz.");
                    }
                    else
                    {
                        var updatedMusteri = MusteriBilgileri.musteriList.SingleOrDefault(x => x.ID == _sozlesmeBilgileri.MusteriID);
                        var musteri = dbFirmaYonetici.Musteri.SingleOrDefault(c => c.ID == _sozlesmeBilgileri.MusteriID);
                        var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.SozlesmeID == _sozlesmeBilgileri.SozlesmeID);
                        var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == sozlesmeYapma.SozlesmeID);

                        if (_sozlesmeBilgileri != null)
                        {

                            sozlesme.ID = _sozlesmeBilgileri.SozlesmeID;
                            sozlesme.SozlesmeAdi = _sozlesmeBilgileri.SozlesmeAdi;
                            sozlesme.SlaSuresi = _sozlesmeBilgileri.SlaSuresi;
                            sozlesme.AnlasmaUcreti = Convert.ToDecimal(_sozlesmeBilgileri.AnlasmaUcreti);
                            //TODO: 2500.250 girince hata veriyor. 

                            sozlesme.BaslangicTarih = _sozlesmeBilgileri.BaslangicTarih;
                            sozlesme.BitisTarih = _sozlesmeBilgileri.BitisTarih;
                            sozlesme.ParcaDahilMi = _sozlesmeBilgileri.ParcaDahilMi;

                            dbFirmaYonetici.Entry(sozlesme).State = EntityState.Modified;
                            dbFirmaYonetici.SaveChanges();

                            musteri.Adres = updatedMusteri.Adres;
                            musteri.Email = updatedMusteri.Email;
                            musteri.MusteriAdi = updatedMusteri.MusteriAdi;
                            musteri.MusteriKodu = updatedMusteri.MusteriKodu;
                            musteri.Password = updatedMusteri.Password;
                            musteri.MusteriTel = updatedMusteri.Tel1;
                            musteri.MusteriTel2 = updatedMusteri.Tel2;
                            musteri.VergiDairesi = updatedMusteri.VergiDairesi;
                            musteri.VergiNumarasi = updatedMusteri.VergiNumarasi;
                            musteri.YetkiliKisi = updatedMusteri.YetkiliKisi;
                            musteri.CreateDate = DateTime.Now;

                            dbFirmaYonetici.Entry(musteri).State = EntityState.Modified;
                            dbFirmaYonetici.SaveChanges();
                            ModelState.Clear();

                           

                        }


                        return RedirectToAction("Index");
                    }
                }
                return View(_sozlesmeBilgileri);


            }
            catch (Exception ex /* dex */)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return View(_sozlesmeBilgileri);
            }


        }

        public ActionResult MusteriEkle(int id = -1)
        {
            var musteri = new MusteriBilgileri();

            try
            {
                if (id == -1)
                {
                    return View(musteri);
                }
                else
                {
                    MusteriBilgileri musteriBilgileri = MusteriBilgileri.musteriList.SingleOrDefault(x => x.ID == id);

                    //var updatedMusteri = dbFirmaYonetici.Musteri.SingleOrDefault(x => x.ID == _musteriBilgileri.ID);

                    musteri.ID = musteriBilgileri.ID;
                    musteri.Adres = musteriBilgileri.Adres;
                    musteri.Email = musteriBilgileri.Email;
                    musteri.MusteriAdi = musteriBilgileri.MusteriAdi;
                    musteri.MusteriKodu = musteriBilgileri.MusteriKodu;
                    musteri.Password = musteriBilgileri.Password;
                    musteri.Tel1 = musteriBilgileri.Tel1;
                    musteri.Tel2 = musteriBilgileri.Tel2;
                    musteri.VergiDairesi = musteriBilgileri.VergiDairesi;
                    musteri.VergiNumarasi = musteriBilgileri.VergiNumarasi;
                    musteri.YetkiliKisi = musteriBilgileri.YetkiliKisi;
                    musteri.CreateDate = DateTime.Now;

                    return View(musteri);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View(musteri);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MusteriEkle(MusteriBilgileri _musteriBilgileri)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MusteriBilgileri musteri = new MusteriBilgileri();

                    var updatedUser = MusteriBilgileri.musteriList.SingleOrDefault(x => x.MusteriKodu == _musteriBilgileri.MusteriKodu);

                    if (updatedUser == null)
                    {
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
                        musteri.CreateUserID = Connection.ID;
                        musteri.CreateDate = DateTime.Now;

                        //TODO: sozlesmeden geri dönerken id yi -1 yap buraada anlasın program sozlesmeden geri donuldugunu if ile kondrol et ona göre ekleme. güncelle.
                        MusteriBilgileri.musteriList.Add(musteri);
                    }
                    else
                    {
                        int index = MusteriBilgileri.musteriList.FindIndex(x => x.MusteriKodu == _musteriBilgileri.MusteriKodu);

                        MusteriBilgileri.musteriList.ToList()[index].Adres = _musteriBilgileri.Adres;
                        MusteriBilgileri.musteriList.ToList()[index].Email = _musteriBilgileri.Email;
                        MusteriBilgileri.musteriList.ToList()[index].MusteriAdi = _musteriBilgileri.MusteriAdi;
                        MusteriBilgileri.musteriList.ToList()[index].MusteriKodu = _musteriBilgileri.MusteriKodu;
                        MusteriBilgileri.musteriList.ToList()[index].Password = _musteriBilgileri.Password;
                        MusteriBilgileri.musteriList.ToList()[index].Tel1 = _musteriBilgileri.Tel1;
                        MusteriBilgileri.musteriList.ToList()[index].Tel2 = _musteriBilgileri.Tel2;
                        MusteriBilgileri.musteriList.ToList()[index].VergiDairesi = _musteriBilgileri.VergiDairesi;
                        MusteriBilgileri.musteriList.ToList()[index].VergiNumarasi = _musteriBilgileri.VergiNumarasi;
                        MusteriBilgileri.musteriList.ToList()[index].YetkiliKisi = _musteriBilgileri.YetkiliKisi;
                        MusteriBilgileri.musteriList.ToList()[index].CreateUserID = Connection.ID;
                        MusteriBilgileri.musteriList.ToList()[index].CreateDate = DateTime.Now;
                    }


                    return RedirectToAction("SozlesmeEkle", "MusteriBilgileri", new { _musteriKodu = _musteriBilgileri.MusteriKodu });
                }
                return View(_musteriBilgileri);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View(_musteriBilgileri);
            }
        }
        public ActionResult SozlesmeEkle(string _musteriKodu)
        {
            SozlesmeBilgileri sozlesme = new SozlesmeBilgileri();

            sozlesme.MusteriKodu = _musteriKodu;
            sozlesme.BaslangicTarih = DateTime.Now;
            sozlesme.BitisTarih = DateTime.Today;
            sozlesme.BitisTarih = Convert.ToDateTime(sozlesme.BitisTarih.ToShortDateString());
            return View(sozlesme);

            //SozlesmeYapma sozlesmeYapma = SozlesmeYapma.sozlesmeYapmaList.SingleOrDefault(x => x.MID == id);
            //SozlesmeBilgileri sozlesme2 = SozlesmeBilgileri.sozlesmeList.SingleOrDefault(x => x.SozlesmeID == 2);

            //var sozlesmeYapma = dbFirmaYonetici.SozlesmeYapma.SingleOrDefault(x => x.MID == id);
            //var sozlesme = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.ID == sozlesmeYapma.SozlesmeID);

            //var sozlesme = SozlesmeBilgileri.sozlesmeList.SingleOrDefault(x => x.SozlesmeID == sozlesme.ID);

            //if (sozlesme != null)
            //{
            //    _sozlesme.SozlesmeID = sozlesme.ID;
            //    _sozlesme.SozlesmeAdi = sozlesme.SozlesmeAdi;

            //    _sozlesme.AnlasmaUcreti = Convert.ToDouble(sozlesme.AnlasmaUcreti);
            //    _sozlesme.SlaSuresi = sozlesme.SlaSuresi;
            //    _sozlesme.BaslangicTarih = sozlesme.BaslangicTarih;

            //    _sozlesme.BitisTarih = sozlesme.BitisTarih;
            //    _sozlesme.ParcaDahilMi = sozlesme.ParcaDahilMi;
            //    _sozlesme.MusteriID = musteri.ID;

            //    //Burayı dusun.
            //}
            //else
            //    return RedirectToAction("Index");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SozlesmeEkle(SozlesmeBilgileri _sozlesme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MusteriBilgileri _musteriBilgileri = MusteriBilgileri.musteriList.SingleOrDefault(x => x.MusteriKodu == _sozlesme.MusteriKodu);

                    bool musteriKoduVarMi = false;

                    var user = dbFirmaYonetici.Musteri.SingleOrDefault(c => c.MusteriKodu == _musteriBilgileri.MusteriKodu);

                    if (user == null) //database de ayni musteri kodu yok. Kayıt yapılabilir.
                    {
                        musteriKoduVarMi = false;

                        var musteri = new Context.Musteri();
                        var sozlesme = new Context.Sozlesme();
                        var sozlesmeYapma = new Context.SozlesmeYapma();

                        musteri.Adres = _musteriBilgileri.Adres;
                        musteri.CreateDate = DateTime.Now;
                        musteri.Email = _musteriBilgileri.Email;
                        musteri.MusteriAdi = _musteriBilgileri.MusteriAdi;
                        musteri.MusteriKodu = _musteriBilgileri.MusteriKodu;
                        musteri.MusteriTel = _musteriBilgileri.Tel1;
                        musteri.MusteriTel2 = _musteriBilgileri.Tel2;
                        musteri.Password = _musteriBilgileri.Password;
                        musteri.VergiDairesi = _musteriBilgileri.VergiDairesi;
                        musteri.VergiNumarasi = _musteriBilgileri.VergiNumarasi;
                        musteri.YetkiliKisi = _musteriBilgileri.YetkiliKisi;

                        sozlesme.AnlasmaUcreti = Convert.ToDecimal(_sozlesme.AnlasmaUcreti);
                        sozlesme.BaslangicTarih = _sozlesme.BitisTarih;
                        sozlesme.BitisTarih = _sozlesme.BitisTarih;
                        sozlesme.ParcaDahilMi = _sozlesme.ParcaDahilMi;
                        sozlesme.SlaSuresi = _sozlesme.SlaSuresi;
                        sozlesme.SozlesmeAdi = _sozlesme.SozlesmeAdi;

                        dbFirmaYonetici.Musteri.Add(musteri);
                        dbFirmaYonetici.Sozlesme.Add(sozlesme);
                        dbFirmaYonetici.SaveChanges();

                        var _musteri = dbFirmaYonetici.Musteri.SingleOrDefault(c => c.MusteriKodu == _musteriBilgileri.MusteriKodu);
                        var sozlesmeAdi = dbFirmaYonetici.Sozlesme.SingleOrDefault(c => c.SozlesmeAdi == _sozlesme.SozlesmeAdi);

                        sozlesmeYapma.MID = _musteri.ID;

                        //EKLEME YAP PDF
                        String FileExt = Path.GetExtension(_sozlesme.files.FileName).ToUpper();

                        if (FileExt == ".PDF")
                        {
                            Stream str = _sozlesme.files.InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                            SozlesmeBilgileri Fd = new SozlesmeBilgileri();
                            Fd.FileName = _sozlesme.files.FileName;
                            Fd.FileContent = FileDet;
                            Fd.MusteriID = _musteri.ID;
                            SaveFileDetails(Fd);
                        }
                        else
                        {
                            ViewBag.FileStatus = "Invalid file format.";
                            return View();
                        }



                        sozlesmeYapma.FyID = Connection.ID;
                        sozlesmeYapma.SozlesmeID = sozlesmeAdi.ID;

                        dbFirmaYonetici.SozlesmeYapma.Add(sozlesmeYapma);
                        dbFirmaYonetici.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else //aynı musteri kodundan varsa kayit yapmadan sayfaya ViewBag.Message yolluyor.
                    {
                        musteriKoduVarMi = true; //database de ayni musteri kodu var.
                        ViewBag.Message = "Farklı bir Musteri Kodu giriniz.";

                        return View(_sozlesme);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bilgilerinizi kontrol ediniz.");
                    return View(_sozlesme);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return View(_sozlesme);
            }
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

                        _sozlesmeList.AnlasmaUcreti = Convert.ToDouble(sozlesme.AnlasmaUcreti);
                        _sozlesmeList.BaslangicTarih = sozlesme.BaslangicTarih;
                        _sozlesmeList.BitisTarih = sozlesme.BitisTarih;
                        _sozlesmeList.SlaSuresi = sozlesme.SlaSuresi;
                        _sozlesmeList.SozlesmeAdi = sozlesme.SozlesmeAdi;
                        _sozlesmeList.SozlesmeID = sozlesme.ID;
                        _sozlesmeList.MusteriKodu = musteri.MusteriKodu;
                        _sozlesmeList.MusteriID = musteri.ID;
                           
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
                            _musteriList.Password2 += "*";
                        }

                        MusteriBilgileri.musteriList.Add(_musteriList);
                        SozlesmeBilgileri.sozlesmeList.Add(_sozlesmeList);
                        SozlesmeYapma.sozlesmeYapmaList.Add(_sozlesmeYapmaList);

                    }
                }
            }

            MusteriBilgileri.musteriList = MusteriBilgileri.musteriList.OrderBy(x => x.CreateDate).ToList();
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

        private List<SozlesmeBilgileri> GetFileList(int mID)
        {
            List<SozlesmeBilgileri> DetList = new List<SozlesmeBilgileri>();
            //List<SozlesmeBilgileri> DetList2 = new List<SozlesmeBilgileri>();

            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<SozlesmeBilgileri>(con, "GetSozlesmePdf", commandType: CommandType.StoredProcedure).ToList().Where(x => x.MusteriID == mID).ToList();
            con.Close();

            //DetList2 = DetList.Where(x => x.MusteriID == mID).ToList();

            return DetList;
        }

        private void SaveFileDetails(SozlesmeBilgileri objDet)
        {
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objDet.FileName);
            Parm.Add("@FileContent", objDet.FileContent);
            Parm.Add("@MusteriID", objDet.MusteriID);
            DbConnection();
            con.Open();
            con.Execute("AddSozlesmePdf", Parm, commandType: System.Data.CommandType.StoredProcedure);
            con.Close();
        }

        private SqlConnection con;
        private string constr;
        private void DbConnection()
        {
            //constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            constr = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
            con = new SqlConnection(constr);

        }
    }
}
