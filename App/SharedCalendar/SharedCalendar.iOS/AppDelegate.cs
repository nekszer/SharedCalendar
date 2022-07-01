using FFImageLoading.Forms.Platform;
using Foundation;
using LightForms;
using SharedCalendar.iOS.Services;
using SharedCalendar.Services;
using UIKit;
using Xamarin.Forms;

namespace SharedCalendar.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPlatformInitializer
    {

        public LocalNotificationImplementation LocalNotification { get; private set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            CachedImageRenderer.Init();
            FormsMaterial.Init();
            Forms.Init();
            LoadApplication(new App(this));
            LocalNotification?.OnLaunching(options);
            return base.FinishedLaunching(app, options);
        }

        public async void RegisterTypes(ICrossContainer container)
        {
            container.Register<ILocalNotification, LocalNotificationImplementation>(FetchTarget.Singleton);
            container.Register<IStatusBarPlatformSpecific, Statusbar>();
            LocalNotification = container.Create<ILocalNotification>() as LocalNotificationImplementation;
            await LocalNotification.Init();
        }
    }
}