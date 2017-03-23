using System.Web.Mvc;

namespace ServisTakipWeb.Areas.Firma
{
    public class FirmaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Firma";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Firma_default",
                "Firma/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
