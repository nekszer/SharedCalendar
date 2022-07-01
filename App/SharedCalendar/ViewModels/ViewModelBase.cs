using LightForms.Services;
using SharedCalendar.Services;
using System;

namespace SharedCalendar.ViewModels
{
    public class ViewModelBase<T> : LightForms.ViewModels.ViewModelBase<T>
    {
        private static IDisposable ProgressDialog { get; set; }

        private IRouteInfo CurrentInfo { get; set; }

        private DateTime AppearingDateTime { get; set; }

        public override void Appearing(string route)
        {
            base.Appearing(route);
            AppearingDateTime = DateTime.Now;
            CurrentInfo = Routing.GetInfo(route);
        }

        public override void Disappearing(string route)
        {
            base.Disappearing(route);
            var elapsedtime = DateTime.Now.Subtract(AppearingDateTime);
            if (CurrentInfo == null) return;
            System.Diagnostics.Debug.WriteLine(CurrentInfo.Title + " | " + elapsedtime.ToString());
            System.Diagnostics.Debug.WriteLine("\t" + CurrentInfo.Description);
        }

        protected override async void ShowProgress()
        {
            base.ShowProgress();
            if (ProgressDialog != null) return;
            ProgressDialog = await Container.Create<ILoadingPopup>().Show();
        }

        protected override void HideProgress()
        {
            base.HideProgress();
            if (ProgressDialog == null) return;
            ProgressDialog.Dispose();
            ProgressDialog = null;
        }
    }
}