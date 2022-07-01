using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedCalendar.Services
{
    public interface IProgressPopup
    {
        Task<IDisposableProgressPopup> Show(string title = null, string message = null, bool iscancelable = false, string cancel = null, CancellationToken token = new CancellationToken());
    }

    public interface IDisposableProgressPopup : IDisposable
    {
        bool SetProgress(int progress);

        void OnCancel(Action action);
    }
}