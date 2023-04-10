using LightForms;
using LightForms.Attributes;
using LightForms.Commands;
using LightForms.Core;
using LightForms.Extensions;
using LightForms.Services;
using LightForms.Validations;
using Newtonsoft.Json;
using SharedCalendar.Extensions;
using SharedCalendar.Models;
using SharedCalendar.Resources;
using SharedCalendar.Services;
using SharedCalendar.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SharedCalendar.ViewModels
{
    public class CalendarViewModel : ViewModelBase<string>
    {
        public SharedCalendar.Controls.Calendar CalendarView { get; private set; }
        public ObservableCollection<Event> EventsOfCalendar { get; private set; }

        public override void Appearing(string route)
        {
            base.Appearing(route);
            var calendar = JsonConvert.DeserializeObject<Calendar>(Parameter);

            EventsOfCalendar = new ObservableCollection<Event>();

            CalendarView = FindViewByName<SharedCalendar.Controls.Calendar>("CalendarView");
            CalendarView.MonthChanged += CalendarView_MonthChanged;
            CalendarView.DaySelected += CalendarView_DaySelected;

            var apiService = Container.Create<IApiService>();
            apiService.GetEventsOfCalendar(calendar.Id).ContinueWith(t =>
            {
                var events = t.Result;
                EventsOfCalendar = new ObservableCollection<Event>(events);
                CalendarView.OnMonthChanged(CalendarView.GetCurrentMonth());
            });
        }

        private void CalendarView_DaySelected(object sender, Controls.IDay e)
        {
            if (e.HasEvents)
                System.Diagnostics.Debug.WriteLine("Tiene Eventos", "Shared Calendar");
        }

        private void CalendarView_MonthChanged(object sender, Controls.IMonth e)
        {
            var startDate = new DateTime(e.Year, e.Number, 1);
            var endDate = new DateTime(e.Year, e.Number, e.DaysInMonth);

            if (EventsOfCalendar.Count < 1)
                return;

            foreach (var day in e.Days)
            {
                System.Diagnostics.Debug.WriteLine(day.Title);
                var eventsOfDay = EventsOfCalendar.Where(@event =>
                {
                    var eventDate = DateTime.Parse(@event.Day);
                    var dayDate = day.Date ?? DateTime.Now;
                    return eventDate.Year == dayDate.Day && eventDate.Month == dayDate.Month && eventDate.Day == dayDate.Day;
                });
                foreach (var @event in eventsOfDay)
                    day.AddEvent(new Controls.Event(@event.Id, @event.Name, @event));
            }
        }
    }
}