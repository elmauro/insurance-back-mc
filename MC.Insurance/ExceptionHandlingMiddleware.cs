using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace insurance_back_mc
{
    public class ExceptionHandlingMiddleware : LoggingMiddleware
    {
        public RequestDelegate requestDelegate { get; set; }
        private readonly ISplunkLogger _splunkLogger;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ISplunkLogger splunkLogger)
        : base(splunkLogger)
        {
            this.requestDelegate = requestDelegate;
            this._splunkLogger = splunkLogger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (CustomException ex)
            {
                await HandleException(context, ex, (int)ex.statusCode);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, 500);
            }
        }
        private Task HandleException(HttpContext context, Exception ex, int statusCode)
        {
            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            Logging("Error", ex.InnerException.Message ?? ex.Message, context);

            return context.Response.WriteAsync(result);
        }
    }
}
