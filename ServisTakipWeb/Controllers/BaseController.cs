using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisTakipWeb.Controllers
{
    public class BaseController : Controller
    { 
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
