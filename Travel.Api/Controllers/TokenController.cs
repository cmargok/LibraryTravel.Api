using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Travel.Application.Security;

namespace Travel.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration config)
        {
            _configuration = config;   
        }


        /// <summary>
        /// Metodo para obtener el token para usar en la Api
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GenerateToken(User user)
        {
            if(user.Name != user.Password)
            {
                return Unauthorized();
            }
            var tokenService = new TokenService(_configuration);
            var token = await tokenService.GetToken();
            return Ok(token);
        }

        
       
    }
}
