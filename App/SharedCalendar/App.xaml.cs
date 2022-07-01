using LightForms;
using LightForms.Services;

namespace SharedCalendar
{
    public partial class App : LightFormsApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        public override void OnInitialized(ICrossContainer container)
        {
            InitializeComponent();
            Config.Instance.OnInintialized(container);
            base.OnInitialized(container);
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnSleep();
        }

        protected override void Routes(IRoutingService routingservice) => new Routes(routingservice);

        protected override void RegisterTypes(ICrossContainer container) => new Dependencies(container);

    }
}
