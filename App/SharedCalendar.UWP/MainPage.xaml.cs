using FFImageLoading.Forms.Platform;
using LightForms;
using SharedCalendar.Services;
using SharedCalendar.UWP.Services;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace SharedCalendar.UWP
{
    public sealed partial class MainPage : IPlatformInitializer
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(480, 1000);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            CachedImageRenderer.Init();
            LoadApplication(new SharedCalendar.App(this));
        }

        public void RegisterTypes(ICrossContainer container)
        {
            container.Register<IStatusBarPlatformSpecific, Statusbar>();
            container.Register<ILocalNotification, LocalNotificationImplementation>(FetchTarget.Singleton);
        }
    }
}