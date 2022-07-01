using System;
using Xamarin.Forms;

namespace SharedCalendar.Themes
{
    public static class RequestTheme
    {
        public static readonly BindableProperty SetProperty =
            BindableProperty.CreateAttached(
                "Set",
                typeof(OSAppTheme),
                typeof(RequestTheme),
                default(OSAppTheme),
                propertyChanged: OnSetChanged);

        public static OSAppTheme GetSet(BindableObject bindable)
            => (OSAppTheme)bindable.GetValue(SetProperty);

        public static void SetSet(BindableObject bindable, OSAppTheme value)
            => bindable.SetValue(SetProperty, value);

        private static void OnSetChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            var viewType = view?.GetType();
            if (viewType?.FullName == null)
                return;
            Enum.TryParse(newValue.ToString(), out OSAppTheme theme);
            BaseTheme.Instance.SetTheme(theme);
        }
    }
}
