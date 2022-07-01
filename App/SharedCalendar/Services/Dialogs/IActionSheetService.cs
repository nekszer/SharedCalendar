using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface IActionSheetPopup
    {
        Task<string> Show(string title, string message, string cancel, params string[] options);
    }
}