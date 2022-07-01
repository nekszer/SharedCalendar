using LightForms;
using LightForms.Commands;
using LightForms.Services;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPopupPage : Xamarin.Forms.ContentPage
    {
        public AlertPopupPage()
        {
            InitializeComponent();
        }
    }

    [Xamarin.Forms.QueryProperty("Title", "title")]
    [Xamarin.Forms.QueryProperty("Message", "message")]
    [Xamarin.Forms.QueryProperty("Cancel", "cancel")]
    public class AlertPopupViewModel : LightForms.ViewModels.ViewModelBase<object>
    {
        #region Notified Property Title
        /// <summary>
        /// Title
        /// </summary>
        private string title;
        public new string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property Message
        /// <summary>
        /// Message
        /// </summary>
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property Cancel
        /// <summary>
        /// Cancel
        /// </summary>
        private string cancel;
        public string Cancel
        {
            get { return cancel; }
            set { cancel = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property Back
        /// <summary>
        /// Back
        /// </summary>
        private ICommand back;
        public ICommand Back
        {
            get { return back; }
            set { back = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            Back = new Command((args) =>
            {
                var navigationservice = CrossContainer.Instance.Create<INavigationService>();
                try { navigationservice.PopModalAsync(); } catch { }
            });
        }
    }

    public class AlertPopup : IAlertPopup
    {

        private const string Name = "/dialogsalert";

        public AlertPopup()
        {
            var routes = CrossContainer.Instance.Create<IRoutingService>();
            if (!routes.RouteItems.ContainsKey(Name))
                routes.Route<AlertPopupPage, AlertPopupViewModel>(Name, true, null, null);
        }

        public Task Show(string title, string message, string cancel)
        {
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            return navigationservice.PushModalAsync(Name + $"?title={title}&message={message}&cancel={cancel}");
        }
    }
}