using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SVX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SVX.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> Register(User user,string role, HttpContext httpContext);
        Task<string> GenerateEmailConfirmationToken(User user);
        Task<string> Authenticate(string username, string password, HttpContext httpContext);
        Task ForgotPassword(HttpContext httpContext,string email);
        Task<IdentityResult> ResetPassword(string email, string token, string newPassword);
    }
}
