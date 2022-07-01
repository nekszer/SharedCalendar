using LightForms;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace SharedCalendar.Services
{
    public class NotificationHandler : INotificationHandler
    {
        public void Init()
        {
            try
            {
                CrossContainer.Instance.Create<ILocalNotification>().NotificationReceived += App_NotificationReceived;
            }catch{ }
        }

        private void App_NotificationReceived(object sender, Dictionary<string, string> data)
        {
            try
            {
                if (!int.TryParse(data[LocalNotificationConstants.IdKey], out int id)) return;
                if (!int.TryParse(data[LocalNotificationConstants.ActionKey], out int idaction)) return;
                var container = CrossContainer.Instance;
                container.Create<ILocalNotification>().Clear(id);
                var action = container.Create<IEnumFactory<NotificationAction, INotificationAction>>()
                    .Resolve((NotificationAction)idaction)
                    .Open();
            }
            catch (Exception ex) { Crashes.TrackError(ex); }
        }
    }
}