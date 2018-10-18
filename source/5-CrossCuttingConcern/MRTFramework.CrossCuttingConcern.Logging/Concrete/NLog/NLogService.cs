using System;
using MRTFramework.CrossCuttingConcern.Logging.Abstract;
using MRTFramework.Model.Enums;

namespace MRTFramework.CrossCuttingConcern.Logging.Concrete.NLog
{
    public class NLogService : ILogger
    {
        private readonly global::NLog.ILogger _logger;
        public NLogService(global::NLog.ILogger logger)
        {
            _logger = logger;
        }
        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Information when IsEnabledFor(LoggingEventType.Information):
                    _logger.Info(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Debug when IsEnabledFor(LoggingEventType.Debug):
                    _logger.Debug(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Warning when IsEnabledFor(LoggingEventType.Warning):
                    _logger.Warn(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Error when IsEnabledFor(LoggingEventType.Error):
                    _logger.Error(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Fatal when IsEnabledFor(LoggingEventType.Fatal):
                    _logger.Fatal(entry.Exception, entry.Message);
                    break;
                case LoggingEventType.Trace when IsEnabledFor(LoggingEventType.Trace):
                    _logger.Trace(entry.Exception, entry.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool IsEnabledFor(LoggingEventType severityType)
        {
            switch (severityType)
            {
                case LoggingEventType.Information:
                    return _logger.IsInfoEnabled;
                case LoggingEventType.Debug:
                    return _logger.IsDebugEnabled;
                case LoggingEventType.Warning:
                    return _logger.IsWarnEnabled;
                case LoggingEventType.Error:
                    return _logger.IsErrorEnabled;
                case LoggingEventType.Fatal:
                    return _logger.IsFatalEnabled;
                case LoggingEventType.Trace:
                    return _logger.IsTraceEnabled;
                default:
                    return false;
            }
        }
    }
}
