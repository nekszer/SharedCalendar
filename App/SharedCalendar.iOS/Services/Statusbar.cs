using Foundation;
using SharedCalendar.iOS.Services;
using SharedCalendar.Services;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SharedCalendar.iOS.Services
{
    public class Statusbar : IStatusBarPlatformSpecific
    {
        public void SetStatusBarColor(Color color, Color foreground)
        {
            try
            {
                if (!(UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView statusBar) || !statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                    return;
                statusBar.BackgroundColor = color.ToUIColor();
                statusBar.TintColor = foreground.ToUIColor();
            }
            catch
            {

            }
        }
    }
}