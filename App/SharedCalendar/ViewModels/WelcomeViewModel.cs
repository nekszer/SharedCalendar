using LightForms;
using LightForms.Attributes;
using LightForms.Commands;
using LightForms.Core;
using LightForms.Extensions;
using LightForms.Services;
using LightForms.Validations;
using SharedCalendar.Extensions;
using SharedCalendar.Models;
using SharedCalendar.Resources;
using SharedCalendar.Services;
using SharedCalendar.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharedCalendar.ViewModels
{
    public class WelcomeViewModel : ViewModelBase<object>
    {
        #region Notified Property Events
        /// <summary>
        /// Events
        /// </summary>
        private ObservableCollection<Event> events;
        public ObservableCollection<Event> Events
        {
            get { return events; }
            set { events = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            Events = new ObservableCollection<Event>();
            var apiService = Container.Create<IApiService>();
            apiService.GetCalendars().ContinueWith(async (t) =>
            {
                var calendars = t.Result ?? new System.Collections.Generic.List<Calendar>();
                foreach (var calendar in calendars)
                {
                    var calendarId = calendar.Id;
                    try
                    {
                        var events = await apiService.GetEventsOfCalendar(calendarId);
                        events.ForEach(e =>
                        {
                            e.CalendarName = calendar.Name;
                            Events.Add(e);
                        });
                    }
                    catch { }
                }
            });
        }
    }
}