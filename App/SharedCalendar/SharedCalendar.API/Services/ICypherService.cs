using System.Threading.Tasks;

namespace SharedCalendar.API.Services
{
    public interface ICypherService
    {
        Task<string> Encrypt(object data);

        Task<string> Decrypt(string data);
    }
}
