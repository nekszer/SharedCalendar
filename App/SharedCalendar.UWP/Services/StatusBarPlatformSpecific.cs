using SharedCalendar.Services;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace SharedCalendar.UWP.Services
{
    public class Statusbar : IStatusBarPlatformSpecific
    {
        public void SetStatusBarColor(Xamarin.Forms.Color background, Xamarin.Forms.Color foreground)
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            var backgroundhex = background.ToHex();
            var foregroundhex = foreground.ToHex();
            var bgcolor = FromHex(backgroundhex);
            var fgcolor = FromHex(foregroundhex);
            titleBar.BackgroundColor = bgcolor;
            titleBar.ButtonBackgroundColor = bgcolor;
            titleBar.ButtonHoverBackgroundColor = fgcolor;
            titleBar.ButtonInactiveBackgroundColor = bgcolor;
            titleBar.ButtonPressedBackgroundColor = fgcolor;
            titleBar.InactiveBackgroundColor = bgcolor;
            titleBar.ForegroundColor = fgcolor;
            titleBar.ButtonForegroundColor = fgcolor;
            titleBar.ButtonHoverForegroundColor = bgcolor;
            titleBar.ButtonInactiveForegroundColor = fgcolor;
            titleBar.ButtonPressedForegroundColor = bgcolor;
            titleBar.InactiveForegroundColor = fgcolor;
        }

        public Color FromHex(string hex)
        {
            var code = hex.Replace("#", string.Empty);
            var r = (byte)System.Convert.ToUInt32(code.Substring(2, 2), 16);
            var g = (byte)System.Convert.ToUInt32(code.Substring(4, 2), 16);
            var b = (byte)System.Convert.ToUInt32(code.Substring(6, 2), 16);
            return Color.FromArgb(255, r, g, b);
        }
    }
}