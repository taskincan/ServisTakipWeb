using ServisTakipWeb.Areas.Admin.Context;
using ServisTakipWeb.Areas.Firma.Context;
using ServisTakipWeb.Areas.FirmaYonetici.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisTakipWeb.Areas.Musteri.Context;
using ServisTakipWeb.Areas.MusteriCalisan.Context; 

namespace ServisTakipWeb.Controllers
{
    public class BaseController : Controller
    {
        private ServisTakipFirmaDBEntities _dbFirma = null;
        private ServisTakipAdminDBEntities _dbAdmin = null;
        private ServisTakipFirmaYoneticiDBEntities _dbFirmaYonetici = null;
        private ServisTakipMusteriDBEntities _dbMusteri = null;
        private ServisTakipMusteriCalisanDBEntities _dbMusteriCalisan = null;

        public ServisTakipFirmaYoneticiDBEntities dbFirmaYonetici
        {
            get
            {
                if (_dbFirmaYonetici == null)
                {
                    _dbFirmaYonetici = new ServisTakipFirmaYoneticiDBEntities();
                    _dbFirmaYonetici.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbFirmaYonetici;
            }
        }

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

        public ServisTakipMusteriDBEntities dbMusteri
        {
            get
            {
                if (_dbMusteri == null)
                {
                    _dbMusteri = new ServisTakipMusteriDBEntities();

                    _dbMusteri.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbMusteri;
            }
        }

        public ServisTakipMusteriCalisanDBEntities dbMusteriCalisan
        {
            get
            {
                if (_dbMusteriCalisan == null)
                {
                    _dbMusteriCalisan = new ServisTakipMusteriCalisanDBEntities();

                    _dbMusteriCalisan.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbMusteriCalisan;
            }
        }

        public BaseController()
        {
            //unitOfWork = new UnitOfWork();
        }

        public String ErrorMessage
        {
            get 
            { 
                return TempData["ErrorMessage"] == null ? String.Empty : TempData["ErrorMessage"].ToString(); 
            }
            set 
            { 
                TempData["ErrorMessage"] = value; 
            }
        }
    }
}
