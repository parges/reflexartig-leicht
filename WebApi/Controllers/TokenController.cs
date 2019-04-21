using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using rl_contract.Models;

namespace kuba_api.Controllers
{
    /// <summary>
    /// Controller für die Token-Verteilung
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration Config { get; }

        #region constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public TokenController(IConfiguration config)
        {
            Config = config;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Rückgabe Token (JWT)
        /// </summary>
        /// <param name="loginModel"></param>
        // TODO: Persistenzschicht für Nutzerverwaltung einbinden (Identity Provider?, Datenbank?)
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody]LoginModel loginModel)
        {
            UserModel userModel = new UserModel()
            {
                Username = "TestUser",
                EMail = "test@localhost",
            };
            var tokenString = BuildToken(userModel);

            return Ok(new { token = tokenString });
        }

        /// <summary>
        /// Testmethode für Token
        /// 200 Ok or 401 Unauthorized
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> CheckAuthedRequest()
        {
            return Ok();
        }

        #endregion

        #region private Methoden

        /// <summary>
        /// Aufbau Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string BuildToken(UserModel user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.EMail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Config["Jwt:Issuer"],
                Config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}