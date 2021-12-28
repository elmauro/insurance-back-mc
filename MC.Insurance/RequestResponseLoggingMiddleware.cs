using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace insurance_back_mc
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISplunkLogger _splunkLogger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ISplunkLogger splunkLogger) {
            _next = next;
            _splunkLogger = splunkLogger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stream originalResBody = context.Response.Body;

            string requestBody = String.Empty;
            string responseBody = String.Empty;

            try
            {
                using (var resStream = new MemoryStream())
                {
                    Stream reqStream = context.Request.Body;
                    context.Response.Body = resStream;

                    await _next(context);

                    resStream.Position = 0;

                    responseBody = new StreamReader(resStream).ReadToEnd();
                    requestBody = await new StreamReader(reqStream).ReadToEndAsync();

                    resStream.Position = 0;

                    await resStream.CopyToAsync(originalResBody);
                }
            }
            finally
            {
                context.Response.Body = originalResBody;

                List<object> args = new List<object>{
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode
                };

                string message;
                switch (context.Request?.Method)
                {
                    case "GET":
                    case "DELETE":
                        message = "Request {method} {url} => {statusCode} {body}";
                        break;

                    default:
                        message = "Request {method} {url} => {statusCode} {requestBody} {responseBody}";
                        args.Add(requestBody);
                        break;

                }

                args.Add(responseBody);

                switch (context.Response.StatusCode) {
                    case 500:
                        _splunkLogger.LogError(
                        message, args.ToArray());
                        break;

                    default:
                        _splunkLogger.LogInformation(
                        message, args.ToArray());
                        break;
                }               
            }
        }
    }
}
