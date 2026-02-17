using Auth.Core.Enums;
using Auth.Core.Services;
using Auth.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly JwtOptions _jwt;

        public AuthController(AuthService auth, IOptions<JwtOptions> jwtOptions)
        {
            _auth = auth;
            _jwt = jwtOptions.Value;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(Models.LoginRequest request)
        {
            var result = await _auth.Login(request.UserName, request.Password);

            if (!result.IsSuccess || string.IsNullOrEmpty(result.Token))
            {
                return Unauthorized(result);
            }
               

            SetJwtCookie(result.Token);
            /*SetCsrfToken();*/
            return Ok(new { isCanEdit = result.IsCanEdit.ToString(),success = true });
        }

       

        

        private void SetJwtCookie(string jwt)
        {
            Response.Cookies.Append("access_token", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(_jwt.AccessTokenMinutes),
                Path = "/"
            });
        }

        /*private void SetCsrfToken()
        {
            var csrf = Guid.NewGuid().ToString();
            Response.Cookies.Append("csrf_token", csrf, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });
        }*/


        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                userName = User.Identity!.Name,
                isCanEdit = User.FindFirst(ClaimTypes.Role)?.Value == UserRole.Admin.ToString()
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("access_token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                Path = "/"
            });

            return Ok(new { success = true });
        }
    }
}
