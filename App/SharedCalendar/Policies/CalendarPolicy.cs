using LightForms;
using Newtonsoft.Json;
using SharedCalendar.Services;
using System;
using System.Threading.Tasks;

namespace SharedCalendar.Policies
{
    public class CalendarPolicy : IPolicy
    {
        public string[] ApplyToRoutes
        {
            get => new string[] { Routes.Calendar };
            set => value = null;
        }

        public Func<IPolicyError, Task> OnError
        {
            get => policyError => CrossContainer.Instance.Create<IToastPopup>().Show("No reconocemos el calendario");
            set => value = null;
        }
        public Func<object, bool> Validation
        {
            get => parameters =>
            {
                var json = parameters?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(json))
                    return false;
                try
                {
                    var calendar = JsonConvert.DeserializeObject<Services.Calendar>(json);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return false;
                }
            };
            set => value = null;
        }

        public QueryToPropertyAttribute[] QueryProperties { get; set; }

    }
}