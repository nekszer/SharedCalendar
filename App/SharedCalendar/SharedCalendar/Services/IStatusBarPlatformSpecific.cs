using Xamarin.Forms;

namespace SharedCalendar.Services
{
    public interface IStatusBarPlatformSpecific
    {
        void SetStatusBarColor(Color background, Color foreground);
    }
}
