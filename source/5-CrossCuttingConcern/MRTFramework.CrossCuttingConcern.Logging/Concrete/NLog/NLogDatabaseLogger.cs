using NLog;

namespace MRTFramework.CrossCuttingConcern.Logging.Concrete.NLog
{
    public class NLogDatabaseLogger : NLogService
    {
        public NLogDatabaseLogger() : base(LogManager.GetLogger("DatabaseLogger"))
        {
        }
    }
}
