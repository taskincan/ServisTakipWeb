using System.Web.Mvc;

namespace ServisTakipWeb.Areas.MusteriCalisan
{
    public class MusteriCalisanAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MusteriCalisan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MusteriCalisan_default",
                "MusteriCalisan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
