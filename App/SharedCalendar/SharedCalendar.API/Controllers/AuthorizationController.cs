using Microsoft.AspNetCore.Mvc;
using SharedCalendar.API.Client.Request;
using SharedCalendar.API.Client.Response;
using SharedCalendar.API.Helpers;
using SharedCalendar.API.Models;
using SharedCalendar.API.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SharedCalendar.API.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        private IDataBaseService Context { get; }

        private ICypherService Cypher { get; }

        public AuthorizationController(IDataBaseService context, ICypherService cypher)
        {
            Context = context;
            Cypher = cypher;
        }

        /// <summary>
        /// Realiza un login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/authorization/signin")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                var user = (await Context.Read($"SELECT * FROM user WHERE username = '{request.UserName}' LIMIT 1")).FirstOrDefault()?.Cast<User>();
                if (user == null) return NotFound("User not found");
                if (user.Password != request.Password) return NotFound("User not found");
                var status = await Context.Update($"UPDATE user SET firebasetoken = '{request.FireBaseToken}' WHERE userid = {user.UserId}");
                var token = await Cypher.Encrypt(user.UserId);
                return StatusCode(200, new AuthorizationResponse
                {
                    Token = token,
                    Status = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Realiza un SignUp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/authorization/signup")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                var userid = await Context.Insert($"INSERT INTO user (`username`, `password`) VALUES ('{request.UserName}', '{request.Password}')");
                if (userid < 0) return StatusCode(500, "INSERT USER ERROR");
                var token = await Cypher.Encrypt(userid);
                return StatusCode(200, new AuthorizationResponse
                {
                    Token = token,
                    Status = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = ex.Message
                });
            }
        }

    }
}
