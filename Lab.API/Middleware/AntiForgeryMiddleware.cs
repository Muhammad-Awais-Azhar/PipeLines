using Microsoft.AspNetCore.Antiforgery;

namespace SVX.API.Middleware
{
    public class AntiForgeryMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-XSS-Protection", "1");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");

            var tokens = _antiforgery.GetAndStoreTokens(context);
            context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false });

            await _next(context);
        }
    }

}
