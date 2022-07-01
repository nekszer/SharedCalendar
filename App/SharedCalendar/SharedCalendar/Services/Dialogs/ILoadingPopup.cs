using System;
using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface ILoadingPopup
    {

        Task<IDisposable> Show(bool iscancelable = false, string cancel = null);

    }
}