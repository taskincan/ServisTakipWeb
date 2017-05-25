using System.Web.Mvc;

namespace ServisTakipWeb.Areas.MusteriYonetici
{
    public class MusteriYoneticiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MusteriYonetici";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MusteriYonetici_default",
                "MusteriYonetici/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
