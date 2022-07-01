using Newtonsoft.Json;
using SharedCalendar.API.Client.Response;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharedCalendar.API.Client.Request
{
    public class SignInRequest : BaseRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FireBaseToken { get; set; }

        public async Task<AuthorizationResponse> ExecuteAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var endpoint = "api/authorization/signin";
                var requestjson = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
                var data = await client.PostAsync($"{BaseUrl}{endpoint}", requestjson);
                if (data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await data.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<AuthorizationResponse>(json);
                    if (response.Status)
                        SetToken(response.Token);
                    return response;
                }
            }
            catch (Exception ex)
            {

            }
            return new AuthorizationResponse
            {
                Status = false
            };
        }
    }
}