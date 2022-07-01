using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Tabs;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnderlineTab : Grid, ISimpleTab
    {

        #region BindableProperty Text
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(UnderlineTab), default(string), BindingMode.OneWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }
        #endregion

        #region BindableProperty UnderlineColor
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(UnderlineTab), Color.Accent, BindingMode.OneWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public Color UnderlineColor
        {
            get
            {
                return (Color)GetValue(UnderlineColorProperty);
            }

            set
            {
                SetValue(UnderlineColorProperty, value);
            }
        }
        #endregion

        public UnderlineTab()
        {
            InitializeComponent();
            BindableProperties = new List<BindablePropertyChanged>
            {
                new BindablePropertyChanged(TextProperty, () => Lbl.Text = Text)
            };
        }

        public void Select(Color color)
        {
            ChangeStatus(color, true);
        }

        public void Unselect(Color color)
        {
            ChangeStatus(color, false);
        }

        private void ChangeStatus(Color color, bool status)
        {
            Lbl.TextColor = color;
            UnderLine.Color = UnderlineColor == Color.Accent ? color : UnderlineColor;
            UnderLine.IsVisible = status;
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