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
        private ServisTakipFirmaEntities _dbFirma = null;
        private ServisTakipAdminEntities _dbAdmin = null;

        public ServisTakipFirmaEntities dbFirma
        {
            get
            {
                if (_dbFirma == null)
                {
                    _dbFirma = new ServisTakipFirmaEntities();
                    _dbFirma.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbFirma;
            }
        }

        public ServisTakipAdminEntities dbAdmin
        {
            get
            {
                if (_dbAdmin == null)
                {
                    _dbAdmin = new ServisTakipAdminEntities();
                    _dbAdmin.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbAdmin;
            }
        }

        //
        // GET: /Giris/

        public ActionResult Index(Giris _girisModel)
        {
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
            bool musteriYoneticisiMi = false, musteriCalisaniMi = false;

            bool userNameVarMi = false;
            bool sifreAyniMi = false;
            bool girisIzni = false;

            if (_girisModel.UserName[0] == 'A' && _girisModel.UserName[1] == 'D' && _girisModel.UserName[2] == 'M' && _girisModel.UserName[3] == '_' ) //Admin Girisi
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
                        }
                        else
                            sifreAyniMi = false;
                    } 
                }                 
            }
            else if (_girisModel.UserName[0]=='F') //Firma Girisi
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
                    if (firmaUserName == dbFirma.Firma.ToList()[temp].YoneticiUserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;
                        if (_girisModel.Password == dbFirma.Firma.ToList()[temp].YoneticiPassword.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;
                            firmaMi = true;

                            Connection.ID = dbFirma.Firma.ToList()[temp].ID;
                            Connection.userName = dbFirma.Firma.ToList()[temp].YoneticiUserName;
                            Connection.adi = dbFirma.Firma.ToList()[temp].FirmaAdi;
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
                else
                    return View("Index", _girisModel);
                //TODO:
                //else if (firmaYoneticisiMi)
                //    return RedirectToAction("Index", "AnaSayfa", new { area = "Firma" });
                //else if (firmaCalisaniMi)
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

        //private string GirisIzni(Giris _girisModel)
        //{
        //    return " ";
        //}

    }
}
