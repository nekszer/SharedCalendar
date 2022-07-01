using LightForms.Services;
using LightForms.ViewModels;
using System;
using System.Linq;

namespace SharedCalendar.Extensions
{
    public static class IRoutingExtensions
    {

        public static void RouteIdiom<ViewModel>(this IRoutingService routing, string routename, bool defaultroute = false,
            Type defaultView = null, Type phone = null, Type tablet = null, Type desktop = null, Type tv = null, Type watch = null,
            IRouteInfo info = null, ILocalizationManager localization = null)
            where ViewModel : IViewModelBase
        {
            switch (Xamarin.Forms.Device.Idiom)
            {
                case Xamarin.Forms.TargetIdiom.Phone:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, phone ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.TargetIdiom.Tablet:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, tablet ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.TargetIdiom.Desktop:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, desktop ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.TargetIdiom.TV:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, tv ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.TargetIdiom.Watch:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, watch ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.TargetIdiom.Unsupported:
                default:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, defaultView, info, localization);
                    break;
            }
        }

        public static void RoutePlatform<ViewModel>(this IRoutingService routing, string routename, bool defaultroute = false,
            Type defaultView = null, Type android = null, Type iOS = null, Type uwp = null, Type macOS = null, Type gtk = null, Type tizen = null, Type wpf = null,
            IRouteInfo info = null, ILocalizationManager localization = null)
            where ViewModel : IViewModelBase
        {
            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.Android:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, android ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.Device.iOS:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, iOS ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.Device.UWP:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, uwp ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.Device.macOS:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, macOS ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.Device.GTK:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, gtk ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.Device.Tizen:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, tizen ?? defaultView, info, localization);
                    break;

                case Xamarin.Forms.Device.WPF:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, wpf ?? defaultView, info, localization);
                    break;

                default:
                    CreateRoute<ViewModel>(routing, routename, defaultroute, defaultView, info, localization);
                    break;
            }
        }

        private static void CreateRoute<ViewModel>(IRoutingService routing, string routename, bool defaultroute, Type view, IRouteInfo info, ILocalizationManager localization)
            where ViewModel : IViewModelBase
        {
            var type = routing.GetType();
            var methods = type.GetMethods();
            var routhemethod = methods.FirstOrDefault(m => m.Name == "Route" && m.GetGenericArguments().Length == 2);
            if (routhemethod == null) return;
            var genericmethod = routhemethod.MakeGenericMethod(view, typeof(ViewModel));
            genericmethod.Invoke(routing, new object[] { routename, defaultroute, info, localization });
        }
    }
}
