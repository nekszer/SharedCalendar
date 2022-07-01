using SharedCalendar.API.Models;
using System.Threading.Tasks;

namespace SharedCalendar.API.Services
{
    public interface IAuthorizationService
    {
        Task<User> GetUsuario(string authorization);
    }
}