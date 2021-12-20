using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.Interfaces.Infrastructure
{
    public interface ISplunkLogger
    {
        void LogInformation(string log);
    }
}
