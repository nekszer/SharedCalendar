using LightForms;
using SharedCalendar.Services;
using System;
using System.Collections.Generic;
using UserNotifications;

namespace SharedCalendar.iOS.Services
{
    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            int.TryParse(notification.Request.Content.Subtitle, out int action);
            CrossContainer.Instance.Create<ILocalNotification>().ReceiveNotification(new Dictionary<string, string>
            {
                { LocalNotificationConstants.TitleKey, notification.Request.Content.Title },
                { LocalNotificationConstants.MessageKey, notification.Request.Content.Body },
                { LocalNotificationConstants.ActionKey, action.ToString() }
            });
            completionHandler(UNNotificationPresentationOptions.Alert);
        }
    }
}