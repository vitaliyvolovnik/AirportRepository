using BLL.Models.AuthModels;
using BLL.Models.Dtos;
using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel register)
        {
            var registered = await _authService.RegisterAsync(register);
            if (registered is null)
                return BadRequest("Can not create user");
            return Ok("user seccessfuly created");
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> LoginAsync(Credentials credentials)
        {
            var logined = await _authService.LoginAsync(credentials);
            if(logined is not null)
            {
                var token = GenerateGwt(logined);

                return Ok(token);
            }

            return BadRequest($"cannot {nameof(LoginAsync)} login");
        }

        [AllowAnonymous]
        [HttpHead("{email}")]
        public async Task<IActionResult> IsEmailexistAsync([FromRoute] string email)
        {
            if (await _authService.IsEmailExistAsync(email))
                return Ok();
            return NotFound();
        }


        [Authorize]
        [HttpHead("{email}/s")]
        public async Task<IActionResult> IsEmailexistsAsync([FromRoute] string email)
        {
            if (await _authService.IsEmailExistAsync(email))
                return Ok();
            return NotFound();
        }




        private string GenerateGwt(UserDto logined)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,logined.Email),
                new Claim(ClaimTypes.Email,logined.Email),
                new Claim(ClaimTypes.GivenName,logined.Firstname),
                new Claim(ClaimTypes.Surname, logined.Lastname),
                new Claim(ClaimTypes.Role, logined.Role)
            };
            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
