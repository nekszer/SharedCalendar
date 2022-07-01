using LightForms;
using LightForms.Commands;
using LightForms.Services;
using LightForms.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms.Xaml;

namespace SharedCalendar.Services
{
    #region View
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressPopupPage : Xamarin.Forms.ContentPage
    {
        public ProgressPopupPage()
        {
            InitializeComponent();
        }
    }
    #endregion

    [Xamarin.Forms.QueryProperty("Message", "message")]
    [Xamarin.Forms.QueryProperty("Cancel", "cancel")]
    [Xamarin.Forms.QueryProperty("IsCancelable", "iscancelable")]
    internal class ProgressPopupViewModel : ViewModelBase<object>
    {
        #region Notified Property Progress
        /// <summary>
        /// Progress
        /// </summary>
        private double progress;
        public double Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged(); }
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

        #region Notified Property BtnCancel
        /// <summary>
        /// BtnCancel
        /// </summary>
        private ICommand btncancel;
        public ICommand BtnCancel
        {
            get { return btncancel; }
            set { btncancel = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property IsCancelable
        /// <summary>
        /// IsCancelable
        /// </summary>
        private bool iscancelable;
        public bool IsCancelable
        {
            get { return iscancelable; }
            set { iscancelable = value; OnPropertyChanged(); }
        }
        #endregion

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

        public override void Appearing(string route)
        {
            base.Appearing(route);
            BtnCancel = new Command(() =>
            {
                if (!IsCancelable) return;
                var navigationservice = CrossContainer.Instance.Create<INavigationService>();
                try { navigationservice.PopModalAsync(); } catch { }
            });
        }
    }

    public class ProgressPopup : IProgressPopup, IDisposableProgressPopup
    {

        private const string Name = "/dialogprogress";

        public ProgressPopup()
        {
            var routes = CrossContainer.Instance.Create<IRoutingService>();
            if (!routes.RouteItems.ContainsKey(Name))
                routes.Route<ProgressPopupPage, ProgressPopupViewModel>(Name, true, null, null);
        }

        public INavigationService Navigationservice { get; set; }

        public bool SetProgress(int progress)
        {
            System.Diagnostics.Debug.WriteLine(progress);
            if (progress <= 0 || progress >= 101)
                return false;
            if (CancellationToken.IsCancellationRequested)
            {
                System.Diagnostics.Debug.WriteLine("IsCancelled");
                Back().ContinueWith(async t =>
                {
                    System.Diagnostics.Debug.WriteLine("Throw Exception");
                    await Task.Delay(2000).ContinueWith(t => CancellationToken.ThrowIfCancellationRequested());
                });
                System.Diagnostics.Debug.WriteLine("False");
                return false;
            }
            if (Navigationservice.ViewModel is not ProgressPopupViewModel viewmodel) return false;
            viewmodel.Progress = progress / 100D;
            System.Diagnostics.Debug.WriteLine("Set " + progress);
            return true;
        }

        private CancellationToken CancellationToken { get; set; }
        public async Task<IDisposableProgressPopup> Show(string title = null, string message = null, bool iscancelable = false, string cancel = null, CancellationToken token = new CancellationToken())
        {
            CancellationToken = token;
            Navigationservice = CrossContainer.Instance.Create<INavigationService>();
            await Navigationservice.PushModalAsync(Name + $"?title={title}&message={message}&cancel={cancel}&iscancelable={iscancelable}");
            return this;
        }

        public void Dispose()
        {
            Back();
        }

        public void OnCancel(Action action)
        {
            CancellationToken.Register(action);
        }

        public Task Back()
        {
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            try { return navigationservice.PopModalAsync(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
            return Task.Delay(1);
        }
    }
}