using MRTFramework.CrossCuttingConcern.Logging.Concrete;
using MRTFramework.Model.Enums;

namespace MRTFramework.CrossCuttingConcern.Logging.Abstract
{
    public interface ILogger
    {
        void Log(LogEntry entry);
        bool IsEnabledFor(LoggingEventType severityType);
    }
}
