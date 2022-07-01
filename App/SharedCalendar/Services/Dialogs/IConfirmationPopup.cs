using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface IConfirmationPopup
    {

        Task<bool> Show(string title, string message, string ok = null, string cancel = null);

    }
}