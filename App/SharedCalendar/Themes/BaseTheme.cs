using LightForms;
using SharedCalendar.Services;
using System;
using Xamarin.Forms;

namespace SharedCalendar.Themes
{
    public class BaseTheme
    {
        private ResourceDictionary Resources
        {
            get => Application.Current.Resources;
        }

        private OSAppTheme Theme
        {
            get => Application.Current.RequestedTheme;
        }

        private BaseTheme()
        {
        }

        private static BaseTheme instance;
        public static BaseTheme Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaseTheme();
                return instance;
            }
        }

        public BaseTheme SetTheme(bool setnavpagestyle = true)
        {
            Application.Current.RequestedThemeChanged += App_RequestedThemeChanged;
            return SetTheme(Theme, setnavpagestyle);
        }

        public T OnTheme<T>(Func<ResourceDictionary, T> @default, Func<ResourceDictionary, T> light = null, Func<ResourceDictionary, T> dark = null)
        {
            var action = Theme == OSAppTheme.Dark ? (dark ?? @default) : (light ?? @default);
            if (action == null) return default(T);
            return action.Invoke(Resources);
        }

        public void LoadResources(OSAppTheme theme)
        {
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(new CommonTheme());
            Resources.MergedDictionaries.Add(theme == OSAppTheme.Light ? new LightTheme() : new DarkTheme());
        }

        public BaseTheme SetTheme(OSAppTheme theme, bool setnavpagestyle = true)
        {
            LoadResources(theme);
            switch (theme)
            {
                default:
                case OSAppTheme.Unspecified:
                case OSAppTheme.Light:
                    var accent = (Color)Resources["Accent"];
                    var onaccent = (Color)Resources["OnAccent"];
                    CrossContainer.Instance.Create<IStatusBarPlatformSpecific>()?.SetStatusBarColor(accent, onaccent);
                    break;

                case OSAppTheme.Dark:
                    CrossContainer.Instance.Create<IStatusBarPlatformSpecific>()?.SetStatusBarColor(Color.Black, Color.White);
                    break;
            }
            if (!setnavpagestyle) return this;
            var navigation = GetNavigationPage();
            if (navigation == null) return this;
            SetNavigationPageStyle(navigation);
            return this;
        }

        public NavigationPage SetNavigationPageStyle(NavigationPage navigation)
        {
            LoadResources(Theme);
            navigation.BarBackgroundColor = Theme == OSAppTheme.Light ?
                       (Color)Resources["Accent"] :
                       (Color)Resources["Surface"];
            navigation.BarTextColor = Theme == OSAppTheme.Light ?
                    (Color)Resources["OnAccent"] :
                    (Color)Resources["OnSurface"];
            return navigation;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
            => SetTheme(e.RequestedTheme);

        private NavigationPage GetNavigationPage()
        {
            var main = Application.Current.MainPage;
            if (main is NavigationPage navpage)
                return navpage;
            else if (main is FlyoutPage master)
                if (master.Detail is NavigationPage navpage1)
                    return navpage1;
            return null;
        }
    }
}