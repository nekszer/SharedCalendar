using Plugin.UI.Xaml.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharedCalendar.Controls
{
    internal class CalendarViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Notified Property Months
        /// <summary>
        /// Months
        /// </summary>
        private IList<IMonth> months;
        public IList<IMonth> Months
        {
            get { return months; }
            set { months = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property Month
        /// <summary>
        /// Month
        /// </summary>
        private IMonth month;
        public IMonth Month
        {
            get { return month; }
            set { month = value; OnPropertyChanged(); if(month != null) OnMonthChanged(value); }
        }
        #endregion

        private void OnMonthChanged(IMonth value)
        {
            Task.Run(() =>
            {
                try
                {
                    if (value == null) return;
                    int row = 0;
                    int col = 0;
                    for (int i = 0; i < value.Days.Count; i++)
                    {
                        var day = value.Days[i];
                        Calendar.AddDay(row, col, day);
                        if ((col + 1) % 7 == 0)
                        {
                            col = 0;
                            row++;
                        }
                        else
                        {
                            col++;
                        }
                    }
                    Calendar.OnMonthChanged(value);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
        }

        #region Notified Property Day
        /// <summary>
        /// Day
        /// </summary>
        private IDay day;
        public IDay Day
        {
            get { return day; }
            set { day = value; OnPropertyChanged(); if (value != null) OnDayChanged(value); }
        }
        #endregion

        #region Notified Property BtnNext
        /// <summary>
        /// BtnNext
        /// </summary>
        private ICommand btnnext;
        public ICommand BtnNext
        {
            get { return btnnext; }
            set { btnnext = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property BtnBack
        /// <summary>
        /// BtnBack
        /// </summary>
        private ICommand btnback;
        public ICommand BtnBack
        {
            get { return btnback; }
            set { btnback = value; OnPropertyChanged(); }
        }
        #endregion

        /// <summary>
        /// Se ejecuta cuando el dia se ha pinchado
        /// </summary>
        /// <param name="day"></param>
        private void OnDayChanged(IDay day)
        {
            if (!day.Date.HasValue)
            {
                SetDayToNull();
                return;
            }
            if (!day.Enabled)
            {
                SetDayToNull();
                return;
            }
            Calendar?.OnDaySelected(day);
            RemoveStyleDaySelection();
            SetStyleDaySelection(day);
            SetDayToNull();
        }

        private void SetDayToNull()
        {
            Day = null;
        }

        private Day CurrentDay { get; set; }

        /// <summary>
        /// Set isselected a true
        /// </summary>
        /// <param name="day"></param>
        private void SetStyleDaySelection(IDay day)
        {
            if (day is Day obj)
            {
                obj.IsSelected = true;
                CurrentDay = obj;
            }
        }

        /// <summary>
        /// Remueve la seleccion de los dias el mes
        /// </summary>
        private void RemoveStyleDaySelection()
        {
            if (CurrentDay == null) return;
            CurrentDay.IsSelected = false;
        }

        internal void SetDay(DateTime selectedDate)
        {
            if (Day != null) return;
            foreach (var month in Months)
                foreach (var day in month.Days)
                    if (day.Date == selectedDate)
                        Day = day;
        }

        private int Index { get; set; }

        private Calendar Calendar { get; }

        internal CalendarViewModel(CultureInfo info, Calendar calendar, DateTime start, DateTime end, IEnumerable<DateTime> disableddates)
        {
            Calendar = calendar;
            Months = info.GetRangeMonthsDataForView(start, end, disableddates).ToList();
            if(Months.Count > 0)
                Month = Months[Index];
            BtnNext = new Command(BtnNext_Clicked);
            BtnBack = new Command(BtnBack_Clicked);
        }

        private void BtnBack_Clicked(object obj)
        {
            if (Index > 0)
            {
                Index--;
                Month = Months[Index];
            }
        }

        private void BtnNext_Clicked(object obj)
        {
            if(Index < Months.Count - 1)
            {
                Index++;
                Month = Months[Index];
            }
        }
    }
}
