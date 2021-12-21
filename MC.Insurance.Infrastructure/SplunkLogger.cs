using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MC.Insurance.Infrastructure
{
    public class SplunkLogger : ISplunkLogger
    {
        Serilog.Core.Logger logger;
        private IOptions<SplunkConfig> settings;

        public SplunkLogger(IOptions<SplunkConfig> settings) {
            this.settings = settings;
            SplunkConfig splunk = this.settings.Value;

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
