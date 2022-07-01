using LightForms;
using LightForms.Commands;
using LightForms.Services;
using LightForms.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationPopupPage : Xamarin.Forms.ContentPage
    {
        public ConfirmationPopupPage()
        {
            InitializeComponent();
        }
    }

    [Xamarin.Forms.QueryProperty("Title", "title")]
    [Xamarin.Forms.QueryProperty("Message", "message")]
    [Xamarin.Forms.QueryProperty("Cancel", "cancel")]
    [Xamarin.Forms.QueryProperty("Ok", "ok")]
    public class ConfirmationPopupViewModel : ViewModelBase<TaskCompletionSource<bool>>
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

        #region Notified Property Ok
        /// <summary>
        /// Ok
        /// </summary>
        private string ok;
        public string Ok
        {
            get { return ok; }
            set { ok = value; OnPropertyChanged(); }
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

        public TaskCompletionSource<bool> BoolTaskCompletionSource { get; private set; }

        public ICommand Back
        {
            get { return back; }
            set { back = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            bool istaskset = false;
            Back = new Command((args) =>
            {
                if (istaskset) return;
                istaskset = true;
                var status = bool.Parse(args.ToString());
                var navigationservice = CrossContainer.Instance.Create<INavigationService>();
                try { navigationservice.PopModalAsync().ContinueWith(t => Parameter.TrySetResult(status)); } catch { }
            });
        }
    }

    public class ConfirmationPopup : IConfirmationPopup
    {
        private const string Name = "/dialogsconfirmation";

        public ConfirmationPopup()
        {
            var routes = CrossContainer.Instance.Create<IRoutingService>();
            if (!routes.RouteItems.ContainsKey(Name))
                routes.Route<ConfirmationPopupPage, ConfirmationPopupViewModel>(Name, true, null, null);
        }

        public Task<bool> Show(string title, string message, string ok = null, string cancel = null)
        {
            TaskCompletionSource<bool> taskcompletionsource = new TaskCompletionSource<bool>();
            if (string.IsNullOrEmpty(ok))
                ok = "Confirm";
            if (string.IsNullOrEmpty(cancel))
                cancel = "Cancel";
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            navigationservice.PushModalAsync(Name + $"?title={title}&message={message}&ok={ok}&cancel={cancel}", taskcompletionsource);
            return taskcompletionsource.Task;
        }
    }
}