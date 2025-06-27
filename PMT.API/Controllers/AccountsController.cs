
using Microsoft.AspNetCore.Mvc;
using PMT.Core.DTOs;
using PMT.Core.ServiceContracts;
using PMT.Core.Services;

namespace PMT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly JwtService _jwtService;

        public AccountsController(IUsersService usersService, JwtService jwtService)
        {
            _usersService = usersService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignIn(SignInRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid sign in request" });

            var signInResult = await _usersService.ValidateLoginCredentialsAsync(model);

            if (!signInResult.IsSuccessful || signInResult.Entity == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var jwtToken = _jwtService.GenerateToken(signInResult.Entity);

            Response.Cookies.Append("jwt", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Ok(new
            {
                message = "User signed in successfully",
                user = signInResult.Entity,
                jwtToken,
            });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Signup(SignupRequest model)
        {
            if (!ModelState.IsValid)
            {
                if (!model.AreTermsNConditionsAccepted)
                {
                    var error = ModelState[nameof(model.AreTermsNConditionsAccepted)]?.Errors.FirstOrDefault()?.ErrorMessage
                                ?? "Terms and conditions must be accepted.";
                    return BadRequest(new { message = error });
                }

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var registerResult = await _usersService.RegisterUserAsync(model);

            if (!registerResult.IsSuccessful || registerResult.Entity == null)
            {
                return BadRequest(new { message = "User registration failed" });
            }

            return Ok(new
            {
                message = "User registered successfully",
                user = registerResult.Entity,
            });
        }

    }
}
