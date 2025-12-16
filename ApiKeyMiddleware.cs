using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace WebApplication1.DAL
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeadName="X-API-Key";
        private readonly string _apikey;
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apikey = configuration["ApiKey"];
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(HeadName,out var extractedKey)||extractedKey!=_apikey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invallid or Wrong API KEY");
                return;
            }
            await _next(context);
        }
    }
}
