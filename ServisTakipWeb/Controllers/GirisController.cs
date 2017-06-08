using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Models;
using ServisTakipWeb.Areas.Admin.Context;
using ServisTakipWeb.Areas.Firma.Context;
using ServisTakipWeb.Areas.FirmaYonetici.Context;

namespace ServisTakipWeb.Controllers
{
    public class GirisController : BaseController
    {
        //
        // GET: /Giris/

        public ActionResult Index(Giris _girisModel)
        {
            Connection.Clear();

            _girisModel.Password = "";
            _girisModel.UserName = "";
            _girisModel.FirmaKodu = "";

            return View(_girisModel);
        }

        [HttpPost]
        public ActionResult Giris(Giris _girisModel)
        {
            //string girisIzniAdi = "";
            //TODO:
            //girisIzniAdi = GirisIzni(_girisModel);

            int temp = 0;
            int count = 0;
            int userNameCount = 0;

            bool adminMi = false;
            bool firmaMi = false, firmaYoneticisiMi = false, firmaCalisaniMi = false;
            bool musteriMi = false, musteriYoneticisiMi = false, musteriCalisaniMi = false;
             
            bool girisIzni = false;

            userNameCount = _girisModel.UserName.Count();

            if (_girisModel.UserName[0] == 'A' && _girisModel.UserName[1] == 'D' && _girisModel.UserName[2] == 'M' && _girisModel.UserName[3] == '_') //Admin Girisi
            {
                adminMi = true;
            }
            else if (_girisModel.UserName[0] == 'F')
            {
                if (userNameCount > 1)
                {
                    if (_girisModel.UserName[1] == 'Y')//Firma Yonetici Girisi
                        firmaYoneticisiMi = true;
                    else if (_girisModel.UserName[1] == 'C')//Firma Calisani Girisi
                        firmaCalisaniMi = true;
                    else //Firma Girisi
                        firmaMi = true;
                }
                else //Firma Girisi
                    firmaMi = true;
            }
            else if (_girisModel.UserName[0] == 'M') //Musteri Girisi
            {
                if (userNameCount > 1)
                {
                    if (_girisModel.UserName[1] == 'Y')//Musteri Yonetici Girisi   
                        musteriYoneticisiMi = true;
                    else if (_girisModel.UserName[1] == 'C') // Musteri Calisani Girisi
                        musteriCalisaniMi = true;
                    else //Musteri Girisi
                        musteriMi = true;
                }
                else //Musteri Girisi
                    musteriMi = true;
            }
            else
            {
                adminMi = false;
                firmaMi = false;
                firmaYoneticisiMi = false;
                firmaCalisaniMi = false;
                musteriMi = false;
                musteriYoneticisiMi = false;
                musteriCalisaniMi = false;

                ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz."); 

                return View("Index", _girisModel);
            }

            if (adminMi) //Admin Girisi
            {
                string adminUserName = "";
                string adminPassword = _girisModel.Password.ToString().Trim();

                count = dbAdmin.Admin.Count();

                for (temp = 4; temp < _girisModel.UserName.Count(); temp++)
                {
                    adminUserName += _girisModel.UserName[temp].ToString();
                }

                var user = dbAdmin.Admin.SingleOrDefault(x => x.UserName == adminUserName);

                if (user == null)
                { 
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz."); 
                    return View("Index", _girisModel);
                }
                else
                {
                    if (_girisModel.Password == user.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    {
                        girisIzni = true;
                        adminMi = true;

                        Connection.ID = user.ID;
                        Connection.userName = user.UserName;
                        Connection.adi = "Admin";

                        return RedirectToAction("Index", "AnaSayfa", new { area = "Admin" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                        return View("Index", _girisModel);
                    }
                }
            }
            else if (firmaMi) //Firma Girisi
            {
                string firmaUserName = "";
                string firmaPassword = _girisModel.Password.ToString().Trim();

                count = dbFirma.Firma.Count();

                for (temp = 1; temp < _girisModel.UserName.Count(); temp++)
                {
                    firmaUserName += _girisModel.UserName[temp].ToString();
                }

                var user = dbFirma.Firma.SingleOrDefault(x => x.UserName == firmaUserName);

                if (user == null)
                {
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                    return RedirectToAction("Index", _girisModel);
                }
                else
                {
                    if (firmaUserName == user.UserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        if (_girisModel.Password == user.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mus sorgusu.
                        {
                            girisIzni = true;
                            firmaMi = true;

                            Connection.ID = user.ID;
                            Connection.userName = user.UserName;
                            Connection.adi = user.FirmaAdi;

                            return RedirectToAction("Index", "AnaSayfa", new { area = "Firma" });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                            return View("Index", _girisModel);
                        }
                    }
                }
            }
            else if (firmaYoneticisiMi) // Firma Yonetici Girisi
            {
                string firmaYoneticiUserName = "";
                string firmaYoneticiPassword = _girisModel.Password.ToString().Trim();

                count = dbFirmaYonetici.FirmaYonetici.Count();

                for (temp = 2; temp < _girisModel.UserName.Count(); temp++)
                {
                    firmaYoneticiUserName += _girisModel.UserName[temp].ToString();
                }
                //TODO: var User = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(x => x.UserName == firmaCalisanUserName); olarak yap hepsini. 

                var user = dbFirmaYonetici.FirmaYonetici.SingleOrDefault(x => x.UserName == firmaYoneticiUserName);

                if (user == null)
                {
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                    return View("Index", _girisModel);
                }
                else
                {
                    if (firmaYoneticiUserName == user.UserName) //database de, girilen kullanici adi varmi sorgusu.
                    { 
                        if (_girisModel.Password == user.Password) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                        {  
                            firmaYoneticisiMi = true;

                            Connection.ID = user.FyID;
                            Connection.userName = user.UserName;
                            Connection.adi = user.Ad;
                            Connection.parentID = user.FirmaID;

                            return RedirectToAction("Index", "AnaSayfa", new { area = "FirmaYonetici" });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                            return View("Index", _girisModel);
                        }
                    }
                }
            }
            else if (firmaCalisaniMi)// Firma Calisani Girisi
            {
                string firmaCalisanUserName = "";
                string firmaCalisanPassword = _girisModel.Password.ToString().Trim();

                count = dbFirmaYonetici.FirmaCalisani.Count();

                for (temp = 2; temp < _girisModel.UserName.Count(); temp++)
                {
                    firmaCalisanUserName += _girisModel.UserName[temp].ToString();
                }

                var User = dbFirmaYonetici.FirmaCalisani.SingleOrDefault(x => x.UserName == firmaCalisanUserName);

                if (User == null)
                {
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                    return View("Index", _girisModel);
                }
                else //Database de aynı kullanıcı adıyla kayıt var.
                { 
                    if (_girisModel.Password == User.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    { 
                        girisIzni = true;
                        firmaYoneticisiMi = true;

                        Connection.ID = User.FcID;
                        Connection.userName = User.UserName;
                        Connection.adi = User.Ad;
                        Connection.parentID = User.FirmaID;

                        return RedirectToAction("Index", "AnaSayfa", new { area = "FirmaCalisan" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                        return View("Index", _girisModel);
                    }
                }
            }
            else if (musteriMi) //
            {
                string musteriUserName = "";
                string musteriPassword = _girisModel.Password.ToString().Trim();

                count = dbFirmaYonetici.Musteri.Count();

                for (temp = 1; temp < _girisModel.UserName.Count(); temp++)
                {
                    musteriUserName += _girisModel.UserName[temp].ToString();
                }

                var User = dbFirmaYonetici.Musteri.SingleOrDefault(x => x.MusteriKodu == musteriUserName);

                if (User == null)
                {
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                    return View("Index", _girisModel);
                }
                else //Database de aynı kullanıcı adıyla kayıt var.
                { 
                    if (_girisModel.Password == User.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    { 
                        girisIzni = true;
                        firmaYoneticisiMi = true;

                        Connection.ID = User.ID;
                        Connection.userName = User.MusteriKodu;
                        Connection.adi = User.MusteriAdi;
                        //Connection.parentID = User.FirmaID;

                        return RedirectToAction("Index", "AnaSayfa", new { area = "Musteri" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                        return View("Index", _girisModel);
                    }
                }

            }
            else if(musteriCalisaniMi)
            {
                string musteriCalisanUserName = "";
                string musteriCalisanPassword = _girisModel.Password.ToString().Trim();

                count = dbMusteri.MusteriCalisani.Count();

                for (temp = 2; temp < _girisModel.UserName.Count(); temp++)
                {
                    musteriCalisanUserName += _girisModel.UserName[temp].ToString();
                }

                var User = dbMusteri.MusteriCalisani.SingleOrDefault(x => x.UserName == musteriCalisanUserName);

                if (User == null) // Girilen bilgiler ile musteri calisani yok.
                {
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                    return View("Index", _girisModel);
                }
                else //Database de aynı kullanıcı adıyla kayıt var.
                {

                    string passwordMD5 = MD5Hash(_girisModel.Password.ToString());
                     
                    if (User.Password.ToString() == passwordMD5) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    { 
                        girisIzni = true;
                        musteriCalisaniMi = true;
                        
                        var musteri = dbMusteri.Musteri.SingleOrDefault(x => x.ID == User.MusteriID);

                        Connection.ID = User.McID;
                        Connection.userName = User.UserName;
                        Connection.adi = musteri.MusteriAdi;
                        Connection.parentID = User.MusteriID;

                        return RedirectToAction("Index", "AnaSayfa", new { area = "MusteriCalisan" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                        return View("Index", _girisModel);
                    }
                }
            }
            else if (musteriYoneticisiMi)
            {
                string musteriYoneticiUserName = "";
                string musteriYoneticiPassword = _girisModel.Password.ToString().Trim();

                count = dbMusteri.MusteriYonetici.Count();

                for (temp = 2; temp < _girisModel.UserName.Count(); temp++)
                {
                    musteriYoneticiUserName += _girisModel.UserName[temp].ToString();
                }

                var User = dbMusteri.MusteriYonetici.SingleOrDefault(x => x.UserName == musteriYoneticiUserName);

                if (User == null) // Girilen bilgiler ile musteri calisani yok.
                {
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                    return View("Index", _girisModel);
                }
                else //Database de aynı kullanıcı adıyla kayıt var.
                {
                    if (_girisModel.Password == User.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    {   
                        Connection.ID = User.MyID;
                        Connection.userName = User.UserName;
                        Connection.adi = User.Ad;
                        Connection.parentID = User.MusteriID;

                        return RedirectToAction("Index", "AnaSayfa", new { area = "MusteriYonetici" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                        return View("Index", _girisModel);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                return View("Index", _girisModel);
            }

            ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                return View("Index", _girisModel);
            
        }
    }
}
