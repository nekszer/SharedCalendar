using System.ComponentModel.DataAnnotations;

namespace SharedCalendar.API.Client.Request
{
    public class SignUpRequest : BaseRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}