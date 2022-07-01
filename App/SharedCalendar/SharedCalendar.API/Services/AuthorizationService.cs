using SharedCalendar.API.Helpers;
using SharedCalendar.API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SharedCalendar.API.Services
{
    public class AuthorizationService : IAuthorizationService
    {

        public IDataBaseService Context { get; }
        public ICypherService Cypher { get; }

        public AuthorizationService(IDataBaseService context, ICypherService cypher)
        {
            Context = context;
            Cypher = cypher;
        }

        public async Task<User> GetUsuario(string authorization)
        {
            try
            {
                var idusuario = await Cypher.Decrypt(authorization);
                var rows = await Context.Read("SELECT * FROM user WHERE userid = " + idusuario);
                var usuario = rows.FirstOrDefault().Cast<User>();
                return usuario;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }
    }
}
