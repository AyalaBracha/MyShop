using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyShop
{
    public class CspMiddleware
    {
        private readonly RequestDelegate _next;
        public CspMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add(
                "Content-Security-Policy",
                "default-src 'self'; script-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline'; img-src 'self'; connect-src 'self' http://localhost:62688 ws://localhost:62688 wss://localhost:44337 https://localhost:44376"
            );
            await _next(context);
        }
    }
}
