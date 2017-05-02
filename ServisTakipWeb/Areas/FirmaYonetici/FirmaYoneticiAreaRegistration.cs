using System.Web.Mvc;

namespace ServisTakipWeb.Areas.FirmaYonetici
{
    public class FirmaYoneticiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FirmaYonetici";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FirmaYonetici_default",
                "FirmaYonetici/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
