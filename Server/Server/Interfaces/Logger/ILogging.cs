using Server.Enums;

namespace Server.Interfaces.Logger
{
    public interface ILogging
    {
        void LogMessage(string message, ELogType logType);
    }
}
