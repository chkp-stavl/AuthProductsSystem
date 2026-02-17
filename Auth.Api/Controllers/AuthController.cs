using System.Security.Claims;
using Auth.Api.Common;
using Auth.Core.Common.Auth.Api.Common;
using Auth.Core.Enums;
using Auth.Core.Services;
using Auth.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly JwtOptions _jwt;

        public AuthController(
            AuthService auth,
            IOptions<JwtOptions> jwtOptions)
        {
            _auth = auth;
            _jwt = jwtOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Models.LoginRequest request)
        {
            try
            {
                var result =
                    await _auth.Login(request.UserName,
                                      request.Password);

                if (!result.IsSuccess
                    || string.IsNullOrEmpty(result.Token))
                {
                    return Unauthorized(result);
                }

                Response.Cookies.Append(
                    AppConstants.AccessTokenCookie,
                    result.Token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires =
                            DateTimeOffset.UtcNow
                                .AddMinutes(_jwt.AccessTokenMinutes),
                        Path = "/"
                    });

                return Ok(new
                {
                    success = true,
                    userName = result.UserName,
                    isCanEdit = result.IsCanEdit
                });
            }
            catch
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ErrorMessages.UnexpectedError
                    });
            }
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            try
            {
                return Ok(new
                {
                    userName = User.Identity?.Name,
                    isCanEdit =
                        User.FindFirst(ClaimTypes.Role)?.Value
                        == UserRole.Admin.ToString()
                });
            }
            catch
            {
                return StatusCode(
                    500,
                     new
                     {
                         message = ErrorMessages.UnexpectedError
                     });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Append(
                    AppConstants.AccessTokenCookie,
                    "",
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires =
                            DateTimeOffset.UtcNow
                                .AddDays(-1),
                        Path = "/"
                    });

                return Ok(new { success = true });
            }
            catch
            {
                return StatusCode(
                    500,
                     new
                     {
                         message = ErrorMessages.UnexpectedError
                     });
            }
        }
    }
}
