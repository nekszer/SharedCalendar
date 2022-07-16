using Plugin.UI.Xaml.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : Grid
    {
        public DayView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is Day day)
            {
                System.Diagnostics.Debug.WriteLine(day.Title, "BindingContext");
            }
        }
    }
}