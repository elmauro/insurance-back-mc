using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
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
        private readonly Microsoft.IO.RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ISplunkLogger splunkLogger) {
            _next = next;
            _splunkLogger = splunkLogger;
            _recyclableMemoryStreamManager = new Microsoft.IO.RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context) {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            
            Logging("Request", ReadStreamInChunks(requestStream), context);

            context.Request.Body.Position = 0;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            
            Logging("Response", text, context);

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private void Logging(string header, string lastArg, HttpContext context) {
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

            switch (header) {
                case "Request":
                    message = $"Http Request Information:{Environment.NewLine}" +
                        message +
                        "Request Body: {Body}";
                    break;

                default:
                    message = $"Http Response Information:{Environment.NewLine}" +
                        message +
                        "Response Body: {Body}";
                    break;
            }


            _splunkLogger.LogInformation(message, args.ToArray());
        }
    }
}
