namespace MC.Insurance.Interfaces.Infrastructure
{
    public interface ISplunkLogger
    {
        void LogInformation(string log, params object[] args);
        void LogError(string log, params object[] args);
    }
}
