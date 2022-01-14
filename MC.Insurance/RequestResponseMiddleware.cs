using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace insurance_back_mc
{
    public class RequestResponseMiddleware : LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microsoft.IO.RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseMiddleware(RequestDelegate next, ISplunkLogger splunkLogger)
        : base(splunkLogger){
            _next = next;
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
            Stream originalBody = context.Response.Body;

            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;

                await _next(context);

                memStream.Position = 0;
                string responseBody = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);

                Logging("Response", responseBody, context);
            }
            context.Response.Body = originalBody;
        }
    }
}
