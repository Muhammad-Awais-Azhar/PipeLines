using SVX.Application.Interfaces;
using SVX.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace SVX.Infrastructure.Services
{
    public class JwtAuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public JwtAuthenticationService(UserManager<User> userManager, IConfiguration configuration, IEmailService emailService, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor, RoleManager<Role> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<string> Authenticate(string username, string password, HttpContext httpContext)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim("role", string.Join(",", roles))
                    };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);
                httpContext.Response.Cookies.Append("jwt", stringToken, new CookieOptions { HttpOnly = true });

                return stringToken;
            }

            return null;
        }

        public async Task<IdentityResult> Register(User user, string? roleName, HttpContext httpContext)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {

                    var role = await _roleManager.FindByNameAsync("Guest");
                    if (role == null)
                    {
                        throw new ApplicationException("Role 'Guest' does not exist.");
                    }
                    var loggedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "System"
                    };
                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<string> GenerateEmailConfirmationToken(User user)
        {
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return emailConfirmationToken;
        }
        public async Task ForgotPassword(HttpContext httpContext, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
                var resetLink = $"{baseUrl}/api/auth/reset-password?email={HttpUtility.UrlEncode(email)}&token={HttpUtility.UrlEncode(token)}";

                var emailSubject = "Password Reset";
                var emailBody = $"Please reset your password by clicking this link: <a href='{resetLink}'>Reset Password</a>";
                await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);
            }
        }
        public async Task<IdentityResult> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Handle invalid or expired reset token
                return IdentityResult.Failed(new IdentityError { Description = "Invalid or expired reset token." });
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result;
        }
    }
}
