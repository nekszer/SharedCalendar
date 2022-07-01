using System;

namespace SharedCalendar.API.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime FromMySqlDateTime(this string value)
        {
            DateTime.TryParse(value, out DateTime date);
            return date;
        }

        public static string ToMySqlDateTime(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}