using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface IApiService
    {
        Task<bool> SignIn(string email, string password);
        Task<bool> SignUp(string email, string password);
        Task<List<Calendar>> GetCalendars();
        Task<List<Event>> GetEventsOfCalendar(int idCalendar);
        bool IsAuthenticated();
    }

    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("allDay")]
        public string AllDay { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("hasLocation")]
        public string HasLocation { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("calendarId")]
        public int CalendarId { get; set; }

        [JsonProperty("accountId")]
        public int AccountId { get; set; }
    }

    public class Calendar
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
    }

    public class AccessToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class Messages
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class InternalServerError
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("error")]
        public int Error { get; set; }
        [JsonProperty("messages")]
        public Messages Messages { get; set; }
    }

    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
    }
}