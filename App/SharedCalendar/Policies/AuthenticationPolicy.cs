using LightForms;
using LightForms.Services;
using SharedCalendar.Services;
using System;
using System.Threading.Tasks;

namespace SharedCalendar.Policies
{
    public class AuthenticationPolicy : IPolicy
    {
        public string[] ApplyToRoutes
        {
            get => new string[] { Routes.Welcome, Routes.Calendar };
            set => value = null;
        }
        
        public Func<IPolicyError, Task> OnError
        {
            get => policyError => CrossContainer.Instance.Create<INavigationService>().PopAsync();
            set => value = null;
        }

        public Func<object, bool> Validation 
        {
            get => parameters => CrossContainer.Instance.Create<IApiService>().IsAuthenticated();
            set => value = null;
        }

        public QueryToPropertyAttribute[] QueryProperties
        {
            get => new QueryToPropertyAttribute[0] { };
            set => value = null;
        }
    }
}