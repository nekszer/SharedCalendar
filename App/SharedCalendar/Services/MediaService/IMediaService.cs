using System.IO;
using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface IMediaService
    {
        Task<Stream> GetMedia(string title, string message, string cancel);
    }
}
