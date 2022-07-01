using SharedCalendar.API.Services;

namespace SharedCalendar.API.Models
{
    public class User
    {
        [ColumnProperty(Name = "userid")]
        public int UserId { get; set; }

        [ColumnProperty(Name = "email")]
        public string Email { get; set; }

        [ColumnProperty(Name = "password")]
        public string Password { get; set; }
    }
}