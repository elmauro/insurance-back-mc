using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace insurance_back_mc
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                var result = JsonConvert.SerializeObject(new { error = "Api Key was not provided. (Using ApiKeyMiddleware)" });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(result);
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = appSettings.GetValue<string>(APIKEYNAME);

            if (!apiKey.Equals(extractedApiKey))
            {
                var result = JsonConvert.SerializeObject(new { error = "Unauthorized client. (Using ApiKeyMiddleware)" });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(result);
                return;
            }

            await _next(context);
        }
    }
}
