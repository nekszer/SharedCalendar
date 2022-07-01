namespace SharedCalendar.ViewModels
{
    public class MainViewModel : ViewModelBase<object>
    {

        #region Notified Property LightFormsVersion
        /// <summary>
        /// LightFormsVersion
        /// </summary>
        private float lightFormsVersion;
        public float LightFormsVersion
        {
            get { return lightFormsVersion; }
            set { lightFormsVersion = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            LightFormsVersion = LocalizationManager.Localize<float>("Versión");
        }
    }
}