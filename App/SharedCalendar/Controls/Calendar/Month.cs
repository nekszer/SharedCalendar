using Plugin.UI.Xaml.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedCalendar.Controls
{
    internal class Month : IMonth
    {
        public int Number { get; }
        public int Year { get; }
        private IList<IDay> Data { get; }
        public IReadOnlyList<IDay> Days
        {
            get
            {
                return Data.ToList();
            }
        }
        public string Title { get; }
        public int DaysInMonth { get; set; }
        public DayOfWeek FirstDay { get; set; }
        public DayOfWeek LastDay { get; set; }

        public Month(string title, int year, int number, int daysinmonth, DayOfWeek firstday, DayOfWeek lastday)
        {
            Title = title;
            Year = year;
            Number = number;
            DaysInMonth = daysinmonth;
            FirstDay = firstday;
            LastDay = lastday;
            Data = new List<IDay>();
        }

        public void AddDay(IDay day)
        {
            Data.Add(day);
        }
    }
}
