using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using LightForms;
using Plugin.CurrentActivity;
using SharedCalendar.Droid.Services;
using SharedCalendar.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using EssentialPlatform = Xamarin.Essentials.Platform;

namespace SharedCalendar.Droid
{
    [Activity(Label = "SharedCalendar", RoundIcon = "@mipmap/ic_launcher", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPlatformInitializer
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            FormsMaterial.Init(this, savedInstanceState);
            EssentialPlatform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App(this));
            CreateNotificationFromIntent(base.Intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            EssentialPlatform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public void RegisterTypes(ICrossContainer container)
        {
            container.Register<ILocalNotification, LocalNotificationImplementation>(FetchTarget.Singleton);
            container.Register<IStatusBarPlatformSpecific, Statusbar>();
        }

        #region Notifications
        protected override void OnNewIntent(Intent intent)
        {
            CreateNotificationFromIntent(intent);
        }

        void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras == null) return;
            string title = intent.Extras.GetString(LocalNotificationConstants.TitleKey);
            string message = intent.Extras.GetString(LocalNotificationConstants.MessageKey);
            int action = intent.Extras.GetInt(LocalNotificationConstants.ActionKey);
            int id = intent.Extras.GetInt(LocalNotificationConstants.IdKey);
            CrossContainer.Instance.Create<ILocalNotification>().ReceiveNotification(new Dictionary<string, string>
            {
                { LocalNotificationConstants.TitleKey, title },
                { LocalNotificationConstants.MessageKey, message },
                { LocalNotificationConstants.ActionKey, action.ToString() },
                { LocalNotificationConstants.IdKey, id.ToString() },
            });
        }
        #endregion
    }
}