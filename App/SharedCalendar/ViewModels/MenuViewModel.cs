using LightForms.Commands;
using LightForms.Services;
using Newtonsoft.Json;
using SharedCalendar.Controls;
using SharedCalendar.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SharedCalendar.ViewModels
{
    public class MenuViewModel : ViewModelBase<object>
    {

        #region Notified Property RouteItems
        /// <summary>
        /// RouteItems
        /// </summary>
        private ObservableCollection<RouteItem> routeitems;
        public ObservableCollection<RouteItem> RouteItems
        {
            get { return routeitems; }
            set { routeitems = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property RouteItem
        /// <summary>
        /// RouteItem
        /// </summary>
        private RouteItem routeitem;
        public RouteItem RouteItem
        {
            get { return routeitem; }
            set { routeitem = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property RouteSelected
        /// <summary>
        /// RouteSelected
        /// </summary>
        private ICommand routeSelected;
        public ICommand RouteSelected
        {
            get { return routeSelected; }
            set { routeSelected = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            RouteSelected = new Command(RouteSelected_Clicked);
            RouteItems = new ObservableCollection<RouteItem>();
            var apiService = Container.Create<IApiService>();
            apiService.GetCalendars().ContinueWith(t =>
            {
                var calendars = t.Result;
                foreach (var item in calendars)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        RouteItems.Add(new RouteItem
                        {
                            Title = item.Name,
                            Route = Routes.Calendar,
                            Icon = Glyph.Calendar,
                            Data = item
                        });
                    });
                }
            });
        }

        /// <summary>
        /// Accion para cuando se selecciona una opcion del menu
        /// </summary>
        /// <param name="obj"></param>
        private async void RouteSelected_Clicked(object obj)
        {
            if (RouteItem == null) return;
            var action = RouteItem.Action;
            var route = RouteItem.Route;
            var ispopup = RouteItem.IsPopup;
            if (string.IsNullOrEmpty(route)) return;
            var flyout = View as Xamarin.Forms.FlyoutPage;
            flyout.IsPresented = false;
            await Task.Run(async () =>
            {
                if (string.IsNullOrEmpty(route)) return;
                if (action != null)
                {
                    var status = await action.Invoke();
                    if (!status)
                        return;
                }
                var json = "";
                if(RouteItem.Data != null)
                    json = JsonConvert.SerializeObject(RouteItem.Data);
                var task = !ispopup ? Navigation.PushAsync(route, json, ReplaceAction.MasterDetailPage) : Navigation.PushModalAsync(route);
                await Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(async () => await task);
            });
        }
    }

    public class RouteItem
    {
        public string Title { get; set; }
        public string Route { get; set; }
        public Func<Task<bool>> Action { get; set; }
        public bool IsPopup { get; set; }
        public Glyph Icon { get; set; }
        public Services.Calendar Data { get; set; }
    }
}