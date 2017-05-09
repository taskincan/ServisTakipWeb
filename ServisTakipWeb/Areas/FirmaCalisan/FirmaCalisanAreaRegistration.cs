using System.Web.Mvc;

namespace ServisTakipWeb.Areas.FirmaCalisan
{
    public class FirmaCalisanAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FirmaCalisan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FirmaCalisan_default",
                "FirmaCalisan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
