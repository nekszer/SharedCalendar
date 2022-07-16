using LightForms;
using LightForms.Attributes;
using LightForms.Commands;
using LightForms.Core;
using LightForms.Extensions;
using LightForms.Services;
using LightForms.Validations;
using SharedCalendar.Extensions;
using SharedCalendar.Models;
using SharedCalendar.Resources;
using SharedCalendar.Services;
using SharedCalendar.ViewModels;

namespace SharedCalendar.ViewModels
{
    public class WelcomeViewModel : ViewModelBase<object>
    {
        public override void Appearing(string route)
        {
            base.Appearing(route);
        }
    }
}