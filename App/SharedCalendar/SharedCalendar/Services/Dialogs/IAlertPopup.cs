using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface IAlertPopup
    {
        Task Show(string title, string message, string cancel);
    }
}