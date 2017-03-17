using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Admin.Models;
using ServisTakipWeb.Areas.Admin.Context;

namespace ServisTakipWeb.Areas.Admin.Controllers
{
    public class AdminBilgileriController : Controller
    {        
        //
        // GET: /Admin/AdminBilgileri/

        public ActionResult Index()
        {
            var context = new ServisTakipWebEntities1();
            ListTemizle();
            ListYarat();
            
            return View(Models.AdminBilgileri.adminList.ToList());
        }
        private void ListYarat()
        {
            int temp;

            var context = new ServisTakipWebEntities1();


            for (temp = 0; temp < context.Admin.Count(); temp++)
            {
                var _adminlist  = new Models.AdminBilgileri();

                _adminlist.ID = context.Admin.ToList()[temp].ID;
                _adminlist.UserName = context.Admin.ToList()[temp].UserName;
                _adminlist.Password = context.Admin.ToList()[temp].Password;
                _adminlist.CreateDate = Convert.ToDateTime(context.Admin.ToList()[temp].CreateDate);

                Models.AdminBilgileri.adminList.Add(_adminlist);
            }
        }

        private void ListTemizle()
        {
            Models.AdminBilgileri.adminList.Clear();
        }
    }
}
