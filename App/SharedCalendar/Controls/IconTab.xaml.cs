using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Tabs;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconTab : Grid, ISimpleTab
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

        #region BindableProperty Glyph
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty IconGlyphProperty = BindableProperty.Create(nameof(IconGlyph), typeof(Glyph), typeof(IconTab), default(Glyph), BindingMode.OneWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public Glyph IconGlyph
        {
            get
            {
                return (Glyph)GetValue(IconGlyphProperty);
            }

            set
            {
                SetValue(IconGlyphProperty, value);
            }
        }
        #endregion

        #region BindableProperty IconSize
        /// <summary>
        /// Description of property
        /// </summary>
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(IconTab), default(double), BindingMode.OneWay);

        /// <summary>
        /// Description of property
        /// </summary>
        public double IconSize
        {
            get
            {
                return (double)GetValue(IconSizeProperty);
            }

            set
            {
                SetValue(IconSizeProperty, value);
            }
        }
        #endregion


        public IconTab()
        {
            InitializeComponent();
            BindableProperties = new List<BindablePropertyChanged>
            {
                new BindablePropertyChanged(TextProperty, () => Lbl.Text = Text),
                new BindablePropertyChanged(IconSizeProperty, () => Icon.Size = IconSize),
                new BindablePropertyChanged(IconGlyphProperty, () => Icon.Glyph = IconGlyph)
            };
        }

        public void Select(Color color)
        {
            ChangeStatus(color);
        }

        public void Unselect(Color color)
        {
            ChangeStatus(color);
        }

        private void ChangeStatus(Color color)
        {
            Lbl.TextColor = color;
            Icon.Color = color;
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