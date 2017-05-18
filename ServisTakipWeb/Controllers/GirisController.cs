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

            bool userNameVarMi = false;
            bool sifreAyniMi = false;
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
                    userNameVarMi = false;

                    ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                    return RedirectToAction("Index", _girisModel);
                }
                else
                {
                    userNameVarMi = true;
                    if (_girisModel.Password == user.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    {
                        sifreAyniMi = true;
                        girisIzni = true;
                        adminMi = true;

                        Connection.ID = user.ID;
                        Connection.userName = user.UserName;
                        Connection.adi = "Admin";

                        return RedirectToAction("Index", "AnaSayfa", new { area = "Admin" });
                    }
                    else
                        sifreAyniMi = false;
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
                    userNameVarMi = false;

                    ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                    return RedirectToAction("Index", _girisModel);
                }
                else
                {
                    if (firmaUserName == user.UserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;

                        if (_girisModel.Password == user.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mus sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;
                            firmaMi = true;

                            Connection.ID = user.ID;
                            Connection.userName = user.UserName;
                            Connection.adi = user.FirmaAdi;

                            return RedirectToAction("Index", "AnaSayfa", new { area = "Firma" });
                        }
                        else
                            sifreAyniMi = false;
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
                    userNameVarMi = false;

                    ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                    return RedirectToAction("Index", _girisModel);
                }
                else
                {
                    if (firmaYoneticiUserName == user.UserName) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;

                        if (_girisModel.Password == user.Password) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;
                            firmaYoneticisiMi = true;

                            Connection.ID = user.FyID;
                            Connection.userName = user.UserName;
                            Connection.adi = user.Ad;
                            Connection.parentID = user.FirmaID;

                            return RedirectToAction("Index", "AnaSayfa", new { area = "FirmaYonetici" });
                        }
                        else
                            sifreAyniMi = false;
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
                    userNameVarMi = false;

                    ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                    return RedirectToAction("Index", _girisModel);
                }
                else //Database de aynı kullanıcı adıyla kayıt var.
                {
                    userNameVarMi = true;

                    if (_girisModel.Password == User.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    {
                        sifreAyniMi = true;
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
                        sifreAyniMi = false;

                        ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                        return RedirectToAction("Index", _girisModel);
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
                    userNameVarMi = false;

                    ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                    return RedirectToAction("Index", _girisModel);
                }
                else //Database de aynı kullanıcı adıyla kayıt var.
                {
                    userNameVarMi = true;

                    if (_girisModel.Password == User.Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                    {
                        sifreAyniMi = true;
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
                        sifreAyniMi = false;

                        ViewBag.Message = "Bilgilerinizi Kontrol Ediniz";
                        return RedirectToAction("Index", _girisModel);
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                return View("Index", _girisModel);
            }

            if (girisIzni)
            {
                if (adminMi)
                    return RedirectToAction("Index", "AnaSayfa", new { area = "Admin" });
                else if (firmaMi)
                    return RedirectToAction("Index", "AnaSayfa", new { area = "Firma" });
                else if (firmaYoneticisiMi)
                    return RedirectToAction("Index", "AnaSayfa", new { area = "FirmaYonetici" });
                else
                    return View("Index", _girisModel);
                //TODO: 
                //else if (firmaCalisaniMi)
                //    return RedirectToAction("Index", "AnaSayfa", new { area = "Firma" });
                //else if (musteriMi)
                //    return RedirectToAction("Index", "AnaSayfa", new { area = "Firma" });
                //else if (musteriYoneticisiMi)
                //    return RedirectToAction("Index", "AnaSayfa", new { area = "Musteri" });
                //else if (musteriCalisaniMi)
                //    return RedirectToAction("Index", "AnaSayfa", new { area = "Musteri" });

            }
            else
            {
                //if(sifreAyniMi==false)
                //    ViewBag.Message = "Şifrenizi kontrol ediniz.";
                //else
                //    ViewBag.Message = "Kullanıcı Adınızı kontrol ediniz.";

                ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz.");
                return View("Index", _girisModel);
            }
        }
    }
}
