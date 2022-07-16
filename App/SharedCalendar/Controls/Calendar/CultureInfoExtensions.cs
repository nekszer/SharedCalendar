using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedCalendar.Controls
{
    public static class CultureInfoExtensions
    {
        public static IReadOnlyList<IMonth> GetRangeMonthsDataForView(this CultureInfo culture, DateTime start, DateTime end, IEnumerable<DateTime> disableddates)
        {
            if (start > end)
                throw new ArgumentException("No puede ser mayor a la fecha de fin (end)", "start");
            var months = new List<IMonth>();
            for (var i = new DateTime(start.Year, start.Month, 1); i < new DateTime(end.Year, end.Month, end.Day); i = i.AddMonths(1))
            {
                months.Add(GetMonthDataForView(culture, i, start, end, disableddates));
            }
            return months;
        }

        public static IMonth GetMonthDataForView(this CultureInfo culture, DateTime show, DateTime start, DateTime end, IEnumerable<DateTime> disableddates)
        {
            if (start > end)
                throw new ArgumentException("No puede ser mayor a la fecha de fin (end)", "start");
            var firstdayofweek = culture.DateTimeFormat.FirstDayOfWeek;
            var toiteration = new DateTime(show.Year, show.Month, 1);
            var weekday = culture.Calendar.GetDayOfWeek(toiteration);

            var positiononweek = (int)weekday - (int)firstdayofweek;

            const int maxdays = 42;
            var daysinmonth = culture.Calendar.GetDaysInMonth(show.Year, show.Month);
            var positionoflastday = daysinmonth + positiononweek;
            var lastdaypositiononweek = (DayOfWeek)(daysinmonth % 7);
            var firstdaypositiononweek = (DayOfWeek)positiononweek;

            var abbreviatedDayNames = culture.DateTimeFormat.AbbreviatedDayNames;

            var month = new Month(show.ToString("Y"), show.Year, show.Month, daysinmonth, firstdaypositiononweek, lastdaypositiononweek);

            Console.WriteLine(month.Title);
            Console.WriteLine();

            for (int i = 0; i < abbreviatedDayNames.Length; i++)
            {
                month.AddDay(new Day(null, abbreviatedDayNames[i], true, false));
                Print(i, abbreviatedDayNames[i], false, true);
            }

            toiteration = toiteration.AddDays(-positiononweek);
            for (int i = 0; i < positiononweek; i++)
            {
                var text = toiteration.Day.ToString();
                month.AddDay(new Day(toiteration, text, false, false));
                Print(i, text);
                toiteration = toiteration.AddDays(1);
            }

            for (int i = positiononweek; i < positionoflastday; i++)
            {
                // TODO: corregir issue de today
                var text = toiteration.Day.ToString();
                // var today = DateTime.Now.Year == toiteration.Year && DateTime.Now.DayOfYear == toiteration.DayOfYear;
                var isenabled = toiteration >= start && toiteration <= end; // || today;
                if(isenabled && disableddates != null && disableddates.Count() > 0)
                    isenabled = !disableddates.Any(d => d.Year == toiteration.Year && d.DayOfYear == toiteration.DayOfYear);
                month.AddDay(new Day(toiteration, text, isenabled, false));
                Print(i, text, false, isenabled);
                toiteration = toiteration.AddDays(1);
            }

            for (int i = positionoflastday; i < maxdays; i++)
            {
                var text = toiteration.Day.ToString();
                month.AddDay(new Day(toiteration, text, false, false));
                Print(i, text);
                toiteration = toiteration.AddDays(1);
            }
            Console.WriteLine();
            return month;
        }

        private static void Print(int i, string text, bool today = false, bool isenabled = false)
        {
            text = today ? "*" + text : text;
            if (isenabled)
                Console.Write($"{text}\t");
            else
                Console.Write($"-\t");

            if (GetModule(i))
                Console.WriteLine("");
        }

        private static bool GetModule(int i)
        {
            return (i + 1) % 7 == 0;
        }
    }
}
