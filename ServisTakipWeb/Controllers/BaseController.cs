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
using ServisTakipWeb.Areas.MusteriYonetici.Context;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net; 

namespace ServisTakipWeb.Controllers
{
    public class BaseController : Controller
    {
        private ServisTakipFirmaDBEntities _dbFirma = null;
        private ServisTakipAdminDBEntities _dbAdmin = null;
        private ServisTakipFirmaYoneticiDBEntities _dbFirmaYonetici = null;
        private ServisTakipMusteriDBEntities _dbMusteri = null;
        private ServisTakipMusteriCalisanDBEntities _dbMusteriCalisan = null;
        private ServisTakipMusteriYoneticiDBEntities _dbMusteriYonetici = null; 

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

        public ServisTakipMusteriYoneticiDBEntities dbMusteriYonetici
        {
            get
            {
                if (_dbMusteriYonetici == null)
                {
                    _dbMusteriYonetici = new ServisTakipMusteriYoneticiDBEntities();

                    _dbMusteriYonetici.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConStr"].ToString();
                }
                return _dbMusteriYonetici;
            }
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static void MailSender(string body, string email)
        {
            var fromAddress = new MailAddress("servistakipyonetimi@gmail.com");
            var toAddress = new MailAddress(email.ToString());
            const string subject = "Servis Takip Sistemi";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "servistakipbitirme")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
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
