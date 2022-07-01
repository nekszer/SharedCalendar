using Microsoft.AspNetCore.Mvc;
using SharedCalendar.API.Models;
using SharedCalendar.API.Services;
using System.Threading.Tasks;

namespace SharedCalendar.API.Controllers
{
    public class AuthorizedBaseController : ControllerBase
    {
        protected IAuthorizationService Authorization { get; }

        protected IDataBaseService Context { get; }

        public AuthorizedBaseController(IAuthorizationService authorization, IDataBaseService context)
        {
            Authorization = authorization;
            Context = context;
        }

        protected User Usuario { get; set; }

        protected async Task<bool> HasAuthorization(string authorization)
        {
            Usuario = await Authorization.GetUsuario(authorization);
            return Usuario != null;
        }
    }
}
