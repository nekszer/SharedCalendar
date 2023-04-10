using Plugin.UI.Xaml.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Controls
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentView
    {

        #region BindableProperty SelectedDate
        /// <summary>
        /// SelectedDate
        /// </summary>
        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(Calendar), default(DateTime), BindingMode.TwoWay);

        /// <summary>
        /// SelectedDate
        /// </summary>
        public DateTime SelectedDate
        {
            get
            {
                return (DateTime)GetValue(SelectedDateProperty);
            }

            set
            {
                SetValue(SelectedDateProperty, value);
            }
        }
        #endregion

        #region BindableProperty StartDate
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty StartDateProperty = BindableProperty.Create(nameof(StartDate), typeof(DateTime?), typeof(Calendar), null, BindingMode.TwoWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public DateTime? StartDate
        {
            get
            {
                return (DateTime?)GetValue(StartDateProperty);
            }

            set
            {
                SetValue(StartDateProperty, value);
            }
        }
        #endregion

        #region BindableProperty EndDate
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty EndDateProperty = BindableProperty.Create(nameof(EndDate), typeof(DateTime?), typeof(Calendar), null, BindingMode.TwoWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public DateTime? EndDate
        {
            get
            {
                return (DateTime?)GetValue(EndDateProperty);
            }

            set
            {
                SetValue(EndDateProperty, value);
            }
        }
        #endregion

        #region DaySelected
        public event EventHandler<IDay> DaySelected;
        internal void OnDaySelected(IDay day)
        {
            if (day == null) return;
            if (!day.Enabled) return;
            if (!day.Date.HasValue) return;
            if (SelectedDate == day.Date.Value) return;
            SelectedDate = day.Date.Value;
            DaySelected?.Invoke(this, day);
        }
        #endregion

        public event EventHandler<IMonth> MonthChanged;
        internal void OnMonthChanged(IMonth month)
        {
            MonthChanged?.Invoke(this, month);
        }

        #region BindableProperty Culture
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty CultureProperty = BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(Calendar), CultureInfo.CurrentCulture, BindingMode.TwoWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                return (CultureInfo)GetValue(CultureProperty);
            }

            set
            {
                SetValue(CultureProperty, value);
            }
        }
        #endregion

        #region BindableProperty DisableDates
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty DisabledDatesProperty = BindableProperty.Create(nameof(DisabledDates), typeof(IEnumerable<DateTime>), typeof(Calendar), default(IEnumerable<DateTime>), BindingMode.OneWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public IEnumerable<DateTime> DisabledDates
        {
            get
            {
                return (IEnumerable<DateTime>)GetValue(DisabledDatesProperty);
            }

            set
            {
                SetValue(DisabledDatesProperty, value);
            }
        }
        #endregion

        #region BindableProperty LeftArrow
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty LeftArrowProperty = BindableProperty.Create(nameof(LeftArrow), typeof(ImageSource), typeof(Calendar), default(ImageSource), BindingMode.TwoWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public ImageSource LeftArrow
        {
            get
            {
                return (ImageSource)GetValue(LeftArrowProperty);
            }

            set
            {
                SetValue(LeftArrowProperty, value);
            }
        }
        #endregion

        #region BindableProperty RightArrow
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty RightArrowProperty = BindableProperty.Create(nameof(RightArrow), typeof(ImageSource), typeof(Calendar), default(ImageSource), BindingMode.TwoWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public ImageSource RightArrow
        {
            get
            {
                return (ImageSource)GetValue(RightArrowProperty);
            }

            set
            {
                SetValue(RightArrowProperty, value);
            }
        }
        #endregion

        internal CalendarViewModel ViewModel { get; set; }

        public Dictionary<string, int> CountExecutions { get; set; }

        public Calendar()
        {
            InitializeComponent();
            CountExecutions = new Dictionary<string, int>();
            SetGrid();
            BindableProperties = new List<BindablePropertyChanged>
            {
                new BindablePropertyChanged(SelectedDateProperty, () =>
                {
                    if(ViewModel == null) return;
                    ViewModel.SetDay(SelectedDate);
                }),
                new BindablePropertyChanged(StartDateProperty, SetViewModel),
                new BindablePropertyChanged(EndDateProperty, SetViewModel),
                new BindablePropertyChanged(CultureProperty, SetViewModel),
                new BindablePropertyChanged(DisabledDatesProperty, SetViewModel),
                new BindablePropertyChanged(LeftArrowProperty, () =>
                {
                    LeftImage.Source = LeftArrow;
                }),
                new BindablePropertyChanged(RightArrowProperty, () =>
                {
                    RightImage.Source = RightArrow;
                })
            };
        }

        public IMonth GetCurrentMonth()
        {
            return ViewModel.Month;
        }

        private void AddCount([CallerMemberName] string name = null)
        {
            if (CountExecutions.ContainsKey(name))
                CountExecutions[name]++;
            else
                CountExecutions.Add(name, 1);
            System.Diagnostics.Debug.WriteLine(name + ": " + CountExecutions[name]);
        }

        private void SetGrid()
        {
            AddCount();
            const int row = 7;
            const int col = 7;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    var view = new DayView();
                    var tapped = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 1,
                        Command = new Command<DayView>((args) => OnDaySelected(args.BindingContext as IDay)),
                        CommandParameter = view
                    };
                    view.GestureRecognizers.Add(tapped);
                    view.BindingContext = new Day(null, string.Empty, false, false);
                    Grid.SetRow(view, i);
                    Grid.SetColumn(view, j);
                    Days.Children.Add(view);
                }
            }
        }

        private void SetViewModel()
        {
            AddCount();
            ViewModel = null;
            Root.BindingContext = null;
            Root.BindingContext = ViewModel = new CalendarViewModel(Culture, this, StartDate.HasValue ? StartDate.Value : DateTime.Now, EndDate.HasValue ? EndDate.Value : DateTime.Now.AddMonths(1), DisabledDates);
        }

        /// <summary>
        /// Método para agregar días
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="day"></param>
        internal void AddDay(int row, int col, IDay day)
        {
            AddCount();
            if (Days == null) return;
            if (Days.Children.Count <= 0) return;
            DayView view = null;
            foreach (var item in Days.Children)
            {
                var itemcol = Grid.GetColumn(item);
                var itemrow = Grid.GetRow(item);
                if (itemcol == col && itemrow == row)
                {
                    view = item as DayView;
                    break;
                }
            }
            var found = view != null ? "View found" : "View Not found";
            if (view == null) return;
            Device.BeginInvokeOnMainThread(() => view.BindingContext = day);
        }

        #region Bindable Property Changed
        public List<BindablePropertyChanged> BindableProperties { get; set; }
        protected override void OnPropertyChanged(string propertyname = null)
        {
            base.OnPropertyChanged(propertyname);
            if (BindableProperties == null) return;
            BindableProperties.FirstOrDefault(b => b.PropertyChanged(propertyname));
        }

        public class BindablePropertyChanged
        {
            protected string PropetyName
            {
                get
                {
                    return SourceProperty?.PropertyName;
                }
            }
            protected BindableProperty SourceProperty;
            protected Action Action { get; set; }

            public BindablePropertyChanged(BindableProperty sourceproperty, Action action)
            {
                SourceProperty = sourceproperty;
                Action = action;
            }

            public bool PropertyChanged(string propertyname)
            {
                if (SourceProperty.PropertyName != propertyname) return false;
                Action?.Invoke();
                return true;
            }
        }
        #endregion
    }
}