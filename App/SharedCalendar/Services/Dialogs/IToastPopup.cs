using System.Threading.Tasks;
using Xamarin.Forms;

namespace SharedCalendar.Services
{
    public interface IToastPopup
    {
        Task Show(string text, int milliseconds = 5000);

        Task Show(View customview, int milliseconds = 5000);
    }
}