using System.IO;

namespace SharedCalendar.API.Client.Request
{
    public class BaseRequest
    {
        internal static void SetEnvironment(ClientEnvironment key)
        {
            switch (key)
            {
                case ClientEnvironment.Development:
                    BaseUrl = "http://192.168.0.4:6411/";
                    break;

                case ClientEnvironment.Production:
                    BaseUrl = "";
                    break;

                default:
                    break;
            }
        }

        internal static void SetToken(string token)
        {
            Authorization = token;
        }

        internal static string GetPath(string uri)
        {
            return Path.Combine(BaseUrl, uri);
        }

        protected static string Authorization { get; set; }
        protected static string BaseUrl { get; set; }
    }

    public enum ClientEnvironment
    {
        Development, Production
    }
}