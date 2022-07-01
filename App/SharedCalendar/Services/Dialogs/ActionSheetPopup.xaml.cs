using LightForms;
using LightForms.Commands;
using LightForms.Services;
using LightForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionSheetPopupPage : Xamarin.Forms.ContentPage
    {
        public ActionSheetPopupPage()
        {
            InitializeComponent();
        }
    }

    [Xamarin.Forms.QueryProperty("Title", "title")]
    [Xamarin.Forms.QueryProperty("Message", "message")]
    [Xamarin.Forms.QueryProperty("Cancel", "cancel")]
    [Xamarin.Forms.QueryProperty("Ok", "ok")]
    internal class ActionSheetPopupViewModel : ViewModelBase<ActionSheetPopupArgs>
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

        #region Notified Property Options
        /// <summary>
        /// Options
        /// </summary>
        private List<string> options;
        public List<string> Options
        {
            get { return options; }
            set { options = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property SelectOption
        /// <summary>
        /// SelectOption
        /// </summary>
        private ICommand selectOption;
        public ICommand SelectOption
        {
            get { return selectOption; }
            set { selectOption = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property Option
        /// <summary>
        /// Option
        /// </summary>
        private string option;
        public string Option
        {
            get { return option; }
            set { option = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property ListHeight
        /// <summary>
        /// ListHeight
        /// </summary>
        private double listHeight;
        public double ListHeight
        {
            get { return listHeight; }
            set { listHeight = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property RowHeight
        /// <summary>
        /// RowHeight
        /// </summary>
        private double rowHeight;
        public double RowHeight
        {
            get { return rowHeight; }
            set { rowHeight = value; OnPropertyChanged(); }
        }
        #endregion

        private ActionSheetPopupArgs ActionSheetPopupArgs { get; set; }

        public override void Appearing(string route)
        {
            base.Appearing(route);
            RowHeight = 50;
            var options = Parameter.Options.ToList();
            options.Add(Cancel);
            Options = options;
            ListHeight = (Options.Count * RowHeight);
            SelectOption = new Command((args) =>
            {
                if (args is not Xamarin.Forms.SelectedItemChangedEventArgs selection)
                    return;
                var opcion = selection?.SelectedItem?.ToString();
                SetResponse(opcion);
            });

            Back = new Command((args) =>
            {
                SetResponse(Cancel);
            });
        }

        private bool IsTaskSet { get; set; }
        private void SetResponse(string response)
        {
            if (IsTaskSet) return;
            IsTaskSet = true;
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            try { navigationservice.PopModalAsync().ContinueWith(t => ActionSheetPopupArgs.TaskCompletionSource.TrySetResult(response)); } catch { }
        }
    }

    public class ActionSheetPopup : IActionSheetPopup
    {
        private const string Name = "/dialogsactionsheet";

        public ActionSheetPopup()
        {
            var routes = CrossContainer.Instance.Create<IRoutingService>();
            if (!routes.RouteItems.ContainsKey(Name))
                routes.Route<ActionSheetPopupPage, ActionSheetPopupViewModel>(Name, true, null, null);
        }

        public Task<string> Show(string title, string message, string cancel, params string[] options)
        {
            TaskCompletionSource<string> taskcompletionsource = new TaskCompletionSource<string>();
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            if (string.IsNullOrEmpty(cancel))
                cancel = "Cancel";
            if (options.Length >= 9)
                throw new Exception("Usa un picker en lugar del actionsheet");
            navigationservice.PushModalAsync(Name + $"?title={title}&message={message}&cancel={cancel}", new ActionSheetPopupArgs
            {
                TaskCompletionSource = taskcompletionsource,
                Options = options
            });
            return taskcompletionsource.Task;
        }
    }

    internal class ActionSheetPopupArgs
    {
        public string[] Options { get; set; }
        public TaskCompletionSource<string> TaskCompletionSource { get; set; }
    }
}