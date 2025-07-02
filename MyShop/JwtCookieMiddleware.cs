using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyShop
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                // אם אין Authorization Header, הוסף אותו מה-Cookie
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }
            await _next(context);
        }
    }
}
