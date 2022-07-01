using Android.App;
using Android.Content;
using SharedCalendar.Droid.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePickerRenderer), new[] { typeof(VisualMarker.MaterialVisual) })]

namespace SharedCalendar.Droid.Controls
{
    public class TimePickerRenderer : MaterialTimePickerRenderer, TimePickerDialog.IOnTimeSetListener
    {
        public TimePickerRenderer(Context context) : base(context)
        {
        }

        protected override TimePickerDialog CreateTimePickerDialog(int hours, int minutes)
        {
            var xamarinview = Element;
            var timepicker = new TimePickerDialog(Context, Resource.Style.AppCompatTimeDialogStyle, (obj, args) =>
            {
                xamarinview.Time = new TimeSpan(args.HourOfDay, args.Minute, 0);
            }, hours, minutes, true);
            return timepicker;
        }
    }
}