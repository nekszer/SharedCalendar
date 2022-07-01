using LightForms;
using LightForms.Animations;
using LightForms.Services;
using SharedCalendar.Themes;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SharedCalendar.Services
{
    public class ToastPopup : IToastPopup
    {
        private View ToastView { get; set; }

        public Task Show(string text, int milliseconds = 5000)
        {
            var toast = GetToast(text);
            return Show(toast, milliseconds);
        }

        public Task Show(View customview, int milliseconds = 5000)
        {
            var grid = GetGrid();
            if (grid == null) return Task.Delay(1000);
            if (ToastView != null)
                grid.Children.Remove(ToastView);
            ToastView = customview;
            customview.Opacity = 0;
            customview.IsVisible = false;
            grid.Children.Add(customview);
            return SetAnimation(customview, new FadeInAnimation
            {
                Duration = "1000",
                Direction = FadeInAnimation.FadeDirection.Up,
                Target = customview
            }, new FadeOutAnimation
            {
                Duration = "1000",
                Direction = FadeOutAnimation.FadeDirection.Up,
                Target = customview,
                Delay = milliseconds
            });
        }

        private Task SetAnimation(View customview, params AnimationBase[] animations)
        {
            try
            {
                var storyboard = new StoryBoard();
                storyboard.Animations.AddRange(animations);
                customview.IsVisible = true;
                return storyboard.Begin();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return Task.Delay(1000);
        }

        private Grid GetGrid()
        {
            var navigationservice = CrossContainer.Instance.Create<INavigationService>();
            var viewmodel = navigationservice.ViewModel;
            var page = viewmodel?.View as Page;
            if (page == null) return null;
            ContentPage contentpage = page == null ? ResolvePage() : page as ContentPage;
            if (contentpage == null) return null;
            Grid grid;
            if (contentpage.Content is Grid pagegrid)
                if (pagegrid.ColumnDefinitions?.Count == 0)
                {
                    grid = pagegrid;
                    return grid;
                }
            grid = new Grid { BackgroundColor = Color.Transparent, Margin = new Thickness(0), Padding = new Thickness(0) };
            grid.Children.Add(contentpage.Content);
            contentpage.Content = grid;
            return grid;
        }

        private ContentPage ResolvePage()
        {
            var page = Application.Current.MainPage;
            if (page is ContentPage content)
                return content;
            if (page is NavigationPage navpage)
                content = navpage.CurrentPage as ContentPage;
            else if (page is FlyoutPage flyoutpage)
                if (flyoutpage.Detail is NavigationPage flynavpage)
                    content = flynavpage.CurrentPage as ContentPage;
                else
                    content = flyoutpage.Detail as ContentPage;
            else if (page is TabbedPage tabbed)
                content = tabbed.CurrentPage as ContentPage;
            else if (page is CarouselPage carousel)
                content = carousel.CurrentPage;
            else
                content = null;
            return content;
        }

        public Frame GetToast(string text)
        {
            var frame = new Frame
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
                Content = new Label
                {
                    Text = text,
                    TextColor = BaseTheme.Instance.OnTheme((dic) => Color.White, (dic) => (Color)dic["OnAccent"], (dic) => (Color)dic["OnSurface"]),
                    MaxLines = 3,
                    LineBreakMode = LineBreakMode.TailTruncation,
                    HorizontalTextAlignment = TextAlignment.Center
                },
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                BackgroundColor = BaseTheme.Instance.OnTheme((dic) => Color.Black, (dic) => (Color)dic["Accent"], (dic) => (Color)dic["Surface"]),
            };
            return frame;
        }
    }
}