using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.Extensions.Options;
using Serilog;

namespace MC.Insurance.Infrastructure
{
    public class SplunkLogger : ISplunkLogger
    {
        readonly Serilog.Core.Logger logger;

        public SplunkLogger(IOptions<SplunkConfig> settings) {
            SplunkConfig splunk = settings.Value;

            this.logger = new LoggerConfiguration()
            .WriteTo.EventCollector(splunk.Url, splunk.Token)
            .CreateLogger();
        }

        public void LogInformation(string log, params object[] args)
        {
            logger.Information(log, args);
        }

        public void LogError(string log, params object[] args)
        {
            logger.Error(log, args);
        }
    }
}
