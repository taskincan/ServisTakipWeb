using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Models;
using ServisTakipWeb.Areas.Admin.Context;
using ServisTakipWeb.Areas.Firma.Context; 

namespace ServisTakipWeb.Controllers
{
    public class GirisController : BaseController
    {
        private ServisTakipFirmaDBEntities _dbFirma = null;
        private ServisTakipAdminDBEntities _dbAdmin = null; 

        public ServisTakipFirmaDBEntities dbFirma
        {
            get
            {
                if (_dbFirma == null)
                {
                    _dbFirma = new ServisTakipFirmaDBEntities();
                    _dbFirma.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbFirma;
            }
        }

        public ServisTakipAdminDBEntities dbAdmin
        {
            get
            {
                if (_dbAdmin == null)
                {
                    _dbAdmin = new ServisTakipAdminDBEntities();

                    _dbAdmin.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbAdmin;
            }
        }
         

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

            bool adminMi = false;
            bool firmaMi = false, firmaYoneticisiMi = false, firmaCalisaniMi = false;
            bool musteriMi=false, musteriYoneticisiMi = false, musteriCalisaniMi = false;

            bool userNameVarMi = false;
            bool sifreAyniMi = false;
            bool girisIzni = false;

            if (_girisModel.UserName[0] == 'A' && _girisModel.UserName[1] == 'D' && _girisModel.UserName[2] == 'M' && _girisModel.UserName[3] == '_') //Admin Girisi
            {
                adminMi = true;
            }
            else if (_girisModel.UserName[0] == 'F')  
            {
                if (_girisModel.UserName[1] == 'Y')//Firma Yonetici Girisi
                    firmaYoneticisiMi = true;
                else if (_girisModel.UserName[1] == 'C')//Firma Calisani Girisi
                    firmaCalisaniMi = true;
                else //Firma Girisi
                    firmaMi = true;
            }
            else if (_girisModel.UserName[0] == 'M') //Musteri Girisi
            {
                if (_girisModel.UserName[1] == 'Y')//Musteri Yonetici Girisi   
                    musteriYoneticisiMi = true;
                else if (_girisModel.UserName[1] == 'C') // Musteri Calisani Girisi
                    musteriCalisaniMi = true;
                else //Musteri Girisi
                    musteriMi = true; 
            }
            else
            {
                adminMi = false;
                firmaMi = false; 
                firmaYoneticisiMi = false; 
                firmaCalisaniMi = false;
                musteriMi=false; 
                musteriYoneticisiMi = false; 
                musteriCalisaniMi = false;
                return View("Index", _girisModel);
            }  

            if ( adminMi ) //Admin Girisi
            { 

                string adminUserName = "";
                string adminPassword = _girisModel.Password.ToString().Trim();
                count = dbAdmin.Admin.Count();

                for (temp = 4; temp < _girisModel.UserName.Count(); temp++)
                {
                    adminUserName += _girisModel.UserName[temp].ToString();
                }

                for (temp = 0; temp < count; temp++)
                {
                    if (adminUserName == dbAdmin.Admin.ToList()[temp].UserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;
                        if (_girisModel.Password == dbAdmin.Admin.ToList()[temp].Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;
                            adminMi = true;

                            Connection.ID = dbAdmin.Admin.ToList()[temp].ID;
                            Connection.userName = dbAdmin.Admin.ToList()[temp].UserName;
                            Connection.adi = "Admin";

                            return RedirectToAction("Index", "AnaSayfa", new { area = "Admin" });
                        }
                        else
                            sifreAyniMi = false;
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

                for (temp = 0; temp < count; temp++)
                {
                    if (firmaUserName == dbFirma.Firma.ToList()[temp].UserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;
                        if (_girisModel.Password == dbFirma.Firma.ToList()[temp].Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mus sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;
                            firmaMi = true;

                            Connection.ID = dbFirma.Firma.ToList()[temp].ID;
                            Connection.userName = dbFirma.Firma.ToList()[temp].UserName;
                            Connection.adi = dbFirma.Firma.ToList()[temp].FirmaAdi; 

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

                count = dbFirma.FirmaYonetici.Count();

                for (temp = 2; temp < _girisModel.UserName.Count(); temp++)
                {
                    firmaYoneticiUserName += _girisModel.UserName[temp].ToString();
                }

                for (temp = 0; temp < count; temp++)
                {
                    if (firmaYoneticiUserName == dbFirma.FirmaYonetici.ToList()[temp].UserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;
                        if (_girisModel.Password == dbFirma.FirmaYonetici.ToList()[temp].Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;
                            firmaYoneticisiMi = true;

                            Connection.ID = dbFirma.FirmaYonetici.ToList()[temp].FyID;
                            Connection.userName = dbFirma.FirmaYonetici.ToList()[temp].UserName;
                            Connection.adi = dbFirma.FirmaYonetici.ToList()[temp].Ad;
                            Connection.parentID = dbFirma.FirmaYonetici.ToList()[temp].FirmaID;

                            return RedirectToAction("Index", "AnaSayfa", new { area = "FirmaYonetici" });
                        }
                        else
                            sifreAyniMi = false;
                    }
                } 
            }
            else if (_girisModel.UserName[0] == 'M')
            {
                return View("Index", _girisModel);
            }
            else
            {
                return View("Index", _girisModel);
            }

            if (girisIzni)
            {
                if(adminMi)
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

                return View("Index", _girisModel);
            } 
        }
    }
}
