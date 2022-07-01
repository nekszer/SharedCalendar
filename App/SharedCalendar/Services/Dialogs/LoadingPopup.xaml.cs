using LightForms;
using LightForms.Commands;
using LightForms.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPopupPage : Xamarin.Forms.ContentPage
    {
        public LoadingPopupPage()
        {
            InitializeComponent();
        }
    }

    [Xamarin.Forms.QueryProperty("Cancel", "cancel")]
    [Xamarin.Forms.QueryProperty("IsCancelable", "iscancelable")]
    public class LoadingPopupViewModel : LightForms.ViewModels.ViewModelBase<object>
    {

        #region Notified Property IsCancelable
        /// <summary>
        /// IsCancelable
        /// </summary>
        private bool isCancelable;
        public bool IsCancelable
        {
            get { return isCancelable; }
            set { isCancelable = value; OnPropertyChanged(); }
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

        #region Notified Property HasMessage
        /// <summary>
        /// HasMessage
        /// </summary>
        private bool hasmessage;
        public bool HasMessage
        {
            get { return hasmessage; }
            set { hasmessage = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            Back = new Command((args) =>
            {
                if (!IsCancelable) return;
                var navigationservice = CrossContainer.Instance.Create<INavigationService>();
                try { navigationservice.PopModalAsync(); } catch { }
            });
        }
    }

    public class LoadingPopupPopup : ILoadingPopup, IDisposable
    {

        private const string Name = "/dialogloading";

        public LoadingPopupPopup()
        {
            var routes = CrossContainer.Instance.Create<IRoutingService>();
            if (!routes.RouteItems.ContainsKey(Name))
                routes.Route<LoadingPopupPage, LoadingPopupViewModel>(Name, true, null, null);
        }

        public void Dispose()
        {
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            try { navigationservice.PopModalAsync(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
        }

        public async Task<IDisposable> Show(bool iscancelable = false, string cancel = null)
        {
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            await navigationservice.PushModalAsync(Name + $"?cancel={cancel}&iscancelable={iscancelable}");
            return this;
        }
    }
}