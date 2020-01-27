using System.Web.Mvc;

namespace TelefonRehber.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Departman_Delete",
                "Admin/{controller}/{action}/{WasDeleted}",
                new { controller = "DepartmanIslem" ,action = "Index", WasDeleted = UrlParameter.Optional }
            );

            context.MapRoute(
                "Personel_Delete",
                "Admin/{controller}/{action}/{WasDeleted}",
                new { controller = "PersonelIslem", action = "Index", WasDeleted = UrlParameter.Optional }
            );
        }
    }
}