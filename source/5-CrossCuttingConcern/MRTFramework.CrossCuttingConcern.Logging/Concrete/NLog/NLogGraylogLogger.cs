using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace MRTFramework.CrossCuttingConcern.Logging.Concrete.NLog
{
    public class NLogGraylogLogger : NLogService
    {
        public NLogGraylogLogger() : base(LogManager.GetLogger("GraylogLogger"))
        {
        }
    }
}
