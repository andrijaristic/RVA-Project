using Serilog;
using Server.Enums;
using Server.Interfaces.Logger;

namespace Server.Logger
{
    public class SerilogLogger : ILogging
    {
        public SerilogLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Year, shared: true)
                .CreateLogger();
        }

        public void LogMessage(string message, ELogType logType)
        {
            if (logType == ELogType.INFO)
            {
                Log.Information(message);
            }

            if (logType == ELogType.ERROR)
            {
                Log.Error(message);
            }
        }
    }
}
