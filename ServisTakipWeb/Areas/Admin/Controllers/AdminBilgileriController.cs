using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Admin.Models;
using ServisTakipWeb.Areas.Admin.Context;
using System.Data.Entity;

namespace ServisTakipWeb.Areas.Admin.Controllers
{
    public class AdminBilgileriController : Controller
    {
        private Context.ServisTakipWebEntities1 db = new Context.ServisTakipWebEntities1();

        //
        // GET: /Admin/AdminBilgileri/

        public ActionResult Index()
        {
            var context = new ServisTakipWebEntities1();
            ListTemizle();
            ListYarat();
            
            return View(Models.AdminBilgileri.adminList.ToList());
        }


        public ActionResult Edit(int id = 0)
        {
            var _adminlist = new Models.AdminBilgileri();

            foreach (var item in Models.AdminBilgileri.adminList)
            {
                if (item.ID == id)
                {
                    _adminlist.ID = item.ID;
                    _adminlist.UserName = item.UserName;
                    _adminlist.Password = item.Password;
                    _adminlist.CreateDate = Convert.ToDateTime(item.CreateDate.ToShortDateString());
                }
            }

            if (_adminlist == null)
            {
                return HttpNotFound();
            }
            return View(_adminlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServisTakipWeb.Areas.Admin.Models.AdminBilgileri _adminBilgileri)
        {
            if (ModelState.IsValid)
            {
                var _user = new Context.Admin();

                _user.ID = _adminBilgileri.ID;

                _user.UserName = _adminBilgileri.UserName;
                _user.Password = _adminBilgileri.Password;
                _user.CreateDate = DateTime.Now;

                //db.Entry(_user).State = EntityState.Modified;

                db.Entry(_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_adminBilgileri);
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
