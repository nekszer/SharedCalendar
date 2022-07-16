using LightForms;
using LightForms.Services;
using Newtonsoft.Json;
using SharedCalendar.Policies;
using SharedCalendar.Resources.Strings;
using SharedCalendar.Services;
using SharedCalendar.ViewModels;
using SharedCalendar.Views;
using System;
using System.Collections.Generic;

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
        public static string SignUp { get; } = "/signUp";

        /// <summary>
        /// Menu de usuario
        /// </summary>
        public static string Menu { get; } = "/menu";

        /// <summary>
        /// Pagina de inicio ya dentro de la app
        /// </summary>
        public static string Welcome { get; } = "/welcome";

        /// <summary>
        /// Calendar
        /// </summary>
        public static string Calendar { get; } = "/calendar";

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
            routingservice.Route<SignUpPage, SignUpViewModel>(SignUp, false);
            routingservice.Route<MenuPage, MenuViewModel>(Menu, false);
            routingservice.Route<WelcomePage, WelcomeViewModel>(Welcome, false);
            routingservice.Route<CalendarPage, CalendarViewModel>(Calendar, false);

            /*
            routingservice.Policies = new List<IPolicy>
            {
                new AuthenticationPolicy(),
                new CalendarPolicy()
            };
            */
        }
    }
}