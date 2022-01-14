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
        public RequestDelegate RequestDelegate { get; set; }

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ISplunkLogger splunkLogger)
        : base(splunkLogger)
        {
            this.RequestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await RequestDelegate(context);
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

            string message = "Source:" + Environment.NewLine;
            message += ex.InnerException?.Source ?? ex.Source;
            message += Environment.NewLine;
            message += "Message:" + Environment.NewLine;
            message += ex.InnerException?.Message ?? ex.Message;
            message += Environment.NewLine;
            message += "StackTrace:" + Environment.NewLine;
            message += ex.InnerException?.StackTrace ?? ex.StackTrace;

            Logging("Error", message, context);

            return context.Response.WriteAsync(result);
        }
    }
}
