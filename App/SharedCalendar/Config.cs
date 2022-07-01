using LightForms;
using SharedCalendar.Services;
using SharedCalendar.Themes;
using System.Globalization;
using Xamarin.Forms;

namespace SharedCalendar
{
    public class Config
    {
        public Environment Environment { get; }

        private Config()
        {
            LightFormsApplication.Instance.Culture = new CultureInfo("es");
            LightFormsApplication.Instance.CustomNavigation = (root) =>
            {
                var navpage = BaseTheme
                    .Instance
                    .SetTheme(true)
                    .SetNavigationPageStyle(new NavigationPage(root));
                return navpage;
            };
            Environment = Environment.Development;
        }

        private static Config instance;
        public static Config Instance
        {
            get
            {
                if (instance == null)
                    instance = new Config();
                return instance;
            }
        }

        public void OnInintialized(ICrossContainer container)
        {
            container.Create<INotificationHandler>()?.Init();
        }
    }
}
