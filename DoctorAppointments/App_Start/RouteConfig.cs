using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoctorAppointments
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "Patients/Login/{username}/{password}/{role}",
                defaults: new { controller = "Patients", action = "Login" }
                );

            routes.MapRoute(
                name: "DeleteApp",
                url: "Appointments/Delete/{id}",
                defaults: new { controller = "Appointments", action = "Delete", id = UrlParameter.Optional }
                );

        }
    }
}
