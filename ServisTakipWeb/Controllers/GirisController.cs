using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Models;
using ServisTakipWeb.Areas.Admin.Context;

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
            
            if (_girisModel.UserName[0] == 'C' && _girisModel.UserName[1] == 'H' && _girisModel.UserName[2] == 'P' && _girisModel.UserName[3] == '_' ) //Admin Girisi
            {
                var context = new ServisTakipWebEntities1();

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
                        }
                        else
                            sifreAyniMi = false;
                    }
                }

                if (girisIzni)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return View("Index", _girisModel);
                }
                
            }
            else // Firma veya Yonetici Girisi
            {
                return View("Index",_girisModel);
            }

            

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
