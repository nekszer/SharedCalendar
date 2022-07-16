using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public class ApiService : IApiService
    {
        private AccessToken AccessToken { get; set; }
        private string EndPoint { get; }
        public ApiService(string endPoint)
        {
            EndPoint = endPoint;
        }

        #region SignIn
        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PostAsync($"{EndPoint}/api/signin", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "email", email },
                    { "password", password }
                }));
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    AccessToken = JsonConvert.DeserializeObject<AccessToken>(json);
                    return !string.IsNullOrEmpty(AccessToken.Token);
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region SignUp
        public async Task<bool> SignUp(string email, string password)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PostAsync($"{EndPoint}/api/signup", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "email", email },
                    { "password", password }
                }));
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    AccessToken = JsonConvert.DeserializeObject<AccessToken>(json);
                    return !string.IsNullOrEmpty(AccessToken.Token);
                }
                var internalservererror = JsonConvert.DeserializeObject<InternalServerError>(json);
                throw new ApiException(internalservererror.Messages.Error);
            }
            catch (Exception ex)
            {
                if(ex is ApiException apiEx)
                    throw apiEx;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region GetCalendars
        public async Task<List<Calendar>> GetCalendars()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-Token", AccessToken.Token);
                var response = await httpClient.GetAsync($"{EndPoint}/api/calendar");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Calendar>>(json);
                }
                return new List<Calendar>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return new List<Calendar>();
            }
        }
        #endregion

        #region Get Events Of Calendar
        public async Task<List<Event>> GetEventsOfCalendar(int idCalendar)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-Token", AccessToken.Token);
                var response = await httpClient.GetAsync($"{EndPoint}/api/event?calendarId={idCalendar}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Event>>(json);
                }
                return new List<Event>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return new List<Event>();
            }
        }
        #endregion

        public bool IsAuthenticated()
        {
            return AccessToken != null;
        }
    }
}