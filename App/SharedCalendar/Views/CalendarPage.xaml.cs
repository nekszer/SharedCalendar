using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SharedCalendar.Views
{
    [DesignTimeVisible(false)]
    public partial class CalendarPage : TabbedPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            CalendarView.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            CalendarView.EndDate = new DateTime(DateTime.Now.Year, 12, 31);
        }
    }
}