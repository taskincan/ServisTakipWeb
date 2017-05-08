using ServisTakipWeb.Areas.FirmaYonetici.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisTakipWeb.Controllers
{
    public class BaseController : Controller
    {
        private ServisTakipFirmaYoneticiDBEntities _dbFirmaYonetici = null;
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
