using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace insurance_back_mc
{
    public class LoggingMiddleware
    {
        private readonly ISplunkLogger _splunkLogger;

        public LoggingMiddleware(ISplunkLogger splunkLogger) {
            _splunkLogger = splunkLogger;
        }

        public void Logging(string header, string lastArg, HttpContext context)
        {
            List<object> args = new List<object>{
                context.Request?.Scheme,
                context.Request?.Host,
                context.Request?.Path.Value,
                context.Request?.QueryString,
                context.Response?.StatusCode,
                lastArg
            };

            string message =
                "Schema:{Scheme} " +
                "Host: {Host} " +
                "Path: {Path} " +
                "QueryString: {QueryString} " +
                "StatusCode: {StatusCode} ";

            switch (header)
            {
                case "Request":
                    message = $"Http Request Information:{Environment.NewLine}" +
                        message +
                        "Request Body: {Body}";

                    _splunkLogger.LogInformation(message, args.ToArray());
                    break;

                case "Response":
                    message = $"Http Response Information:{Environment.NewLine}" +
                        message +
                        "Response Body: {Body}";

                    _splunkLogger.LogInformation(message, args.ToArray());
                    break;

                default:
                    message = $"Http Error:{Environment.NewLine}" +
                        message +
                        "Response Body: {Body}";

                    _splunkLogger.LogError(message, args.ToArray());
                    break;
            }
        }
    }
}
