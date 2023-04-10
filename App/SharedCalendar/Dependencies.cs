using LightForms;
using SharedCalendar.Extensions;
using SharedCalendar.Services;

namespace SharedCalendar
{
    public class Dependencies
    {
        public Dependencies(ICrossContainer container)
        {
            // Shared dependencies
            /// Alerts
            container.Register<IToastPopup, ToastPopup>(FetchTarget.Singleton);
            container.Register<IConfirmationPopup, ConfirmationPopup>();
            container.Register<IAlertPopup, AlertPopup>();
            container.Register<IActionSheetPopup, ActionSheetPopup>();
            container.Register<ILoadingPopup, LoadingPopupPopup>();
            container.Register<IProgressPopup, ProgressPopup>();


            /// Media
            container.Register<IMediaService, MediaService>();

            /// Notifications
            container.Register<INotificationHandler, NotificationHandler>();


            // Factories
            /// Media
            container.RegisterFactory<MediaSource, IStreamSource>();

            /// Notifications
            container.RegisterFactory<NotificationAction, INotificationAction>();

            /// APIs
            container.Register<IApiService>((container) =>
            {
                var apiService = new ApiService("http://sharedcalendar.aliensofttech.space");
                apiService.SetStorage(new ApiStorage());
                apiService.LoadStorage();
                return apiService;
            }, FetchTarget.Singleton);
        }
    }
}
