using BLL.Models.AuthModels;
using BLL.Models.Dtos;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AirportApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;

        public AuthController(IAuthService authService, IConfiguration config,IUserService userService,IEmployeeService employeeService)
        {
            _authService = authService;
            _config = config;
            _userService = userService;
            _employeeService = employeeService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel register)
        {
            var registered = await _authService.RegisterAsync(register);
            if (registered is null)
                return BadRequest();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> LoginAsync(Credentials credentials)
        {
            var user = await _authService.LoginAsync(credentials);

            if (user == null)
                return NotFound();

            var token = GenerateGwt(user, user?.Employee);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userService.UpdateUserAsync(user, user.Id);

            return Ok(new AuthenticatedResponse()
            {
                RefreshToken = refreshToken,
                User = user,
                Token = token
            });
        }

        [AllowAnonymous]
        [HttpHead("{email}")]
        public async Task<IActionResult> IsEmailexistAsync([FromRoute] string email)
        {
            if (await _authService.IsEmailExistAsync(email))
                return Ok();
            return NotFound();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshModel refreshModel)
        {
            var principal = GetPrincipalFromExpiredToken(refreshModel.Token);

            if (principal?.Identity?.Name is null)
                return Unauthorized();

            var user = await _userService.GetUserAsync(principal.Identity.Name);

            if (user is null ||
                user.RefreshToken != refreshModel.RefreshToken ||
                user.RefreshTokenExpiry < DateTime.UtcNow)
                return Unauthorized();
            var token = GenerateGwt(user);

            return Ok(new AuthenticatedResponse()
            {
                RefreshToken = refreshModel.RefreshToken,
                Token = token
            });
        }

        [Authorize]
        [HttpDelete("revoke")]
        public async Task<IActionResult> Revoke()
        {

            var username = HttpContext.User.Identity?.Name;

            if (username is null)
                return Unauthorized();

            var user = await _userService.GetUserAsync(username);

            if (user is null)
                return Unauthorized();

            user.RefreshToken = string.Empty;
            await _userService.UpdateUserAsync(user, user.Id);

            return Ok();
        }


        [AllowAnonymous]
        [HttpPatch("confirm/{token}")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] string token)
        {
            if (string.IsNullOrWhiteSpace(token)) 
                return BadRequest();

            var userDto = await _authService.ConfirmTokenAsync(token);
            if(userDto is null)
                return NotFound();

            return Ok();
        }



        private string GenerateGwt(UserDto logined, EmployeeDto? employee = null)
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
            if(employee is not null)
            {
                claims.Append(new Claim(ClaimTypes.Role, employee.Post));
            }

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var validation = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }
    }
}
