using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Admin.Models;
using ServisTakipWeb.Areas.Admin.Context;
using System.Data.Entity;
//using ServisTakipWeb.Areas.Firma.Context;
using ServisTakipWeb.Controllers;

namespace ServisTakipWeb.Areas.Admin.Controllers
{
    public class AdminBilgileriController : BaseController
    {
        private ServisTakipAdminDBEntities _db = null;

        public ServisTakipAdminDBEntities db
        {
            get
            {
                if (_db == null)
                {
                    _db = new ServisTakipAdminDBEntities();
                    _db.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _db;
            }
        }

       // private Context.ServisTakipAdminEntities db = new Context.ServisTakipAdminEntities();

        //
        // GET: /Admin/AdminBilgileri/

        public ActionResult Index()
        { 
            ListTemizle();
            ListYarat();
            
            return View(Models.AdminBilgileri.adminList.ToList());
        }

        public ActionResult Edit(int id = -1)
        {
            var _adminlist = new AdminBilgileri();

            foreach (var item in AdminBilgileri.adminList)
            {
                if (item.ID == id)
                {
                    _adminlist.ID = item.ID;
                    _adminlist.UserName = item.UserName;
                    _adminlist.Password = item.Password;
                    _adminlist.CreateDate = Convert.ToDateTime(item.CreateDate.ToShortDateString());
                }
            }

            if (_adminlist.ID == 0)
            {
                return RedirectToAction("Index");
                //return HttpNotFound();
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
                 
                db.Entry(_user).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(_adminBilgileri);
        }

        private void ListYarat()
        {
            int temp, count=0;

            count = db.Admin.Count();

            for (temp = 0; temp < count; temp++)
            {
                var _adminlist  = new Models.AdminBilgileri();
                  
                _adminlist.ID = db.Admin.ToList()[temp].ID;
                _adminlist.UserName = db.Admin.ToList()[temp].UserName;
                _adminlist.Password = db.Admin.ToList()[temp].Password;
                _adminlist.CreateDate = Convert.ToDateTime(db.Admin.ToList()[temp].CreateDate);

                Models.AdminBilgileri.adminList.Add(_adminlist);
            }
        }

        private void ListTemizle()
        {
            Models.AdminBilgileri.adminList.Clear();
        }
    }
}
