using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SVX.API.Models;
using SVX.Application.Interfaces;
using SVX.Domain.Entities;
using SVX.Infrastructure.Services;
using System.Threading.Tasks;

namespace SVX.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public AuthController(IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authenticationService.Authenticate(loginRequest.Username, loginRequest.Password, _httpContextAccessor.HttpContext);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new { token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {

            var registrationResult = await _authenticationService.Register(user,"", _httpContextAccessor.HttpContext);

            if (registrationResult.Succeeded)
            {
                // Generate the email confirmation token and send the confirmation email
                var token = await _authenticationService.GenerateEmailConfirmationToken(user);
                var confirmationLink = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    new { userId = user.Id, token },
                    Request.Scheme);
                var emailSubject = "Confirm your email";
                var emailBody = $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>";
                await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);

                return Ok(new { message = "Registration successful. Please check your email for confirmation." });
            }

            return BadRequest(registrationResult.Errors);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            await _authenticationService.ForgotPassword(HttpContext, model.Email);

            // Return a success response
            return Ok(new { message = "Password reset instructions have been sent to your email." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
         
            var result = await _authenticationService.ResetPassword(model.Email, model.Token, model.NewPassword);

            if (result.Succeeded)
            {
                // Password reset successful
                return Ok(new { message = "Password reset successful." });
            }

            // Password reset failed
            return BadRequest(new { errors = result.Errors });
        }
    }
}
