using LightForms.Services;
using SharedCalendar.Resources.Strings;
using SharedCalendar.ViewModels;
using SharedCalendar.Views;

namespace SharedCalendar
{
    public class Routes
    {

        /// <summary>
        /// Pagina principal de la app
        /// </summary>
        public static string Main { get; } = "/main";

        /// <summary>
        /// Pagina first
        /// </summary>
        public static string First { get; } = "/first";

        /// <summary>
        /// RoutingService
        /// </summary>
        /// <param name="routingservice"></param>
        public Routes(IRoutingService routingservice)
        {
            routingservice.Route<MainPage, MainViewModel>(Main, true, new RouteInfo
            {
                Title = "Página principal",
                Description = "La descripción de la página principal"
            }, new JsonLocalizationManager("Main.json"));
        }
    }
}