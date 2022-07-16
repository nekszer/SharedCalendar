using System;
using System.Collections.Generic;

namespace SharedCalendar.Controls
{
    public interface IMonth
    {
        IReadOnlyList<IDay> Days { get; }
        string Title { get; }
        int Number { get; }
        int Year { get; }
        int DaysInMonth { get; }
        DayOfWeek FirstDay { get; }
        DayOfWeek LastDay { get; }
        void AddDay(IDay day);
    }
}
