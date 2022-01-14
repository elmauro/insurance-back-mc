using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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
            string authorization = context.Request?.Headers["Authorization"];
            string userId = string.Empty;

            if (authorization != null && authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorization.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                userId = jwtSecurityToken.Claims.First(x => x.Type == "nombre").Value;
            }

            List<object> args = new List<object>{
                userId,
                context.Request?.Scheme,
                context.Request?.Host,
                header == "Request" ? context.Request?.Headers : context.Response?.Headers,
                context.Request?.Path.Value,
                context.Request?.Method,
                context.Request?.QueryString,
                context.Response?.StatusCode,
                context.Request?.Path.Value == "/login" && header != "Error" ? String.Empty :lastArg
            };

            string message =
                "UserId:{UserId} " +
                "Schema:{Scheme} " +
                "Host: {Host} " +
                "Headers: {Headers} " +
                "Path: {Path} " +
                "Method: {Method} " +
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
