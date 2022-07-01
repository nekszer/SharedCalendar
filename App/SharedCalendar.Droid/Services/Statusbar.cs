using Android.OS;
using Plugin.CurrentActivity;
using SharedCalendar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SharedCalendar.Droid.Services
{
    public class Statusbar : IStatusBarPlatformSpecific
    {
        public void SetStatusBarColor(Color color, Color foreground)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) return;
            var androidColor = color.AddLuminosity(-0.1).ToAndroid();
            CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);
            CrossCurrentActivity.Current.Activity.Window.SetTitleColor(foreground.ToAndroid());
        }
    }
}