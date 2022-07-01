using System.IO;
using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface IStreamSource
    {
        Task<Stream> Get();
    }
}