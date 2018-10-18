using NLog;

namespace MRTFramework.CrossCuttingConcern.Logging.Concrete.NLog
{
    public class NLogFileLogger : NLogService
    {
        public NLogFileLogger() : base(LogManager.GetLogger("FileLogger"))
        {
        }
    }
}
