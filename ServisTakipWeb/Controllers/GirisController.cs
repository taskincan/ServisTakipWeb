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
    public class GirisController : Controller
    {
        //
        // GET: /Giris/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(Giris _girisModel)
        {
            int temp;

            bool adminMi = false;
            bool firmaYoneticisiMi = false, firmaCalisaniMi = false;
            bool musteriYoneticisiMi = false, musteriCalisaniMi = false ;
            
            bool userNameVarMi = false;
            bool sifreAyniMi = false;
            bool girisIzni = false;
            
            if (_girisModel.UserName[0] == 'A' && _girisModel.UserName[1] == 'D' && _girisModel.UserName[2] == 'M' && _girisModel.UserName[3] == '_' ) //Admin Girisi
            {
                var context = new ServisTakipAdminEntities();

                string adminUserName = ""; 
                string adminPassword = _girisModel.Password.ToString().Trim();

                for (temp = 4; temp < _girisModel.UserName.Count(); temp++)
                {
                    adminUserName += _girisModel.UserName[temp].ToString();
                }

                for (temp = 0; temp < context.Admin.Count() ; temp++)
                {
                    if (adminUserName == context.Admin.ToList()[temp].UserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;
                        if (_girisModel.Password == context.Admin.ToList()[temp].Password.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;

                            Connection.ID = context.Admin.ToList()[temp].ID;
                            Connection.userName = context.Admin.ToList()[temp].UserName;
                        }
                        else
                            sifreAyniMi = false;
                    }
                }

                if (girisIzni)
                {
                    
                    return RedirectToAction("Index", "AnaSayfa", new { area = "Admin" });
                }
                else
                {
                    return View("Index", _girisModel);
                }
                
            }
            else if (_girisModel.UserName[0]=='F') //Firma Girisi
            {
                var context = new ServisTakipFirmaEntities();

                string firmaUserName = "";
                string firmaPassword = _girisModel.Password.ToString().Trim();

                for (temp = 1; temp < _girisModel.UserName.Count(); temp++)
                {
                    firmaUserName += _girisModel.UserName[temp].ToString();
                }

                for (temp = 0; temp < context.Firma.Count(); temp++)
                {
                    if (firmaUserName == context.Firma.ToList()[temp].YoneticiUserName.ToString()) //database de, girilen kullanici adi varmi sorgusu.
                    {
                        userNameVarMi = true;
                        if (_girisModel.Password == context.Firma.ToList()[temp].YoneticiPassword.ToString()) //Database de ki kullanici adinin şifresi ile eşleşiyor mu sorgusu.
                        {
                            sifreAyniMi = true;
                            girisIzni = true;

                            Connection.ID = context.Firma.ToList()[temp].ID;
                            Connection.userName = context.Firma.ToList()[temp].YoneticiUserName;
                        }
                        else
                            sifreAyniMi = false;
                    }
                }

                if (girisIzni)
                {

                    return RedirectToAction("Index", "AnaSayfa", new { area = "Firma" });
                }
                else
                {
                    return View("Index", _girisModel);
                }
            }
            else if(_girisModel.UserName[0]=='M')
            {
                return View("Index",_girisModel);
            }
            else
                return View("Index", _girisModel);

            //if (girisIzni == true) //Girilen bilgiler ile sisteme giris yapilabilir.
            //{
            //    if (count == 0) // admin girisi
            //    {
            //        //Connection.whereAmI = "admin";
            //        //return RedirectToAction("Index", "A_AnaSayfa");
            //    }
            //    else
            //    {
            //        //Connection.whereAmI = "user";
            //        //return RedirectToAction("Index", "U_AnaSayfa");
            //    }


            //}
            //else //Boyle bir kullanici yok, Kullanici bulunamadi mesaji verilecek.
            //{
            //    ////return RedirectToAction("Index", "Girisler");
            //    //if (!ModelState.IsValid)
            //    //{
            //    //    ViewBag.Message = "Kullanıcı Adı veya Şifrenizi kontrol ediniz.";
            //    //    return View("Index", _loginModel);
            //    //}
            //    //ViewBag.Message = "Kullanıcı Adı veya Şifreniz yanlış girildi.";
            //    //return View("Index", _loginModel);
            //}
        }

    }
}
