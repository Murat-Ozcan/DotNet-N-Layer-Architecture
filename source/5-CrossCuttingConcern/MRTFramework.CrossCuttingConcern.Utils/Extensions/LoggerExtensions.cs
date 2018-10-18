using System;
using System.Diagnostics;
using System.Linq;
using MRTFramework.CrossCuttingConcern.Logging.Abstract;
using MRTFramework.CrossCuttingConcern.Logging.Concrete;
using MRTFramework.Model.Enums;
using MRTFramework.Model.InApps.LogModel;
using PostSharp.Aspects;

namespace MRTFramework.CrossCuttingConcern.Utils.Extensions
{
    public static class LoggerExtensions
    {
        public static LogDetail LogMethodDetail(this MethodExecutionArgs args, string customMessage = null, string aspectName = null)
        {
            var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
            {
                Name = t.Name,
                Type = t.ParameterType.Name,
                Value = args.Arguments.GetArgument(i)
            }).ToList();

            var logDetail = new LogDetail()
            {
                NameSpace = args.Method.DeclaringType?.FullName,
                MethodName = args.Method.Name,
                CodeLine = args.Exception == null ? string.Empty :
                new StackTrace(args.Exception, true).GetFrame(0).GetFileLineNumber().ToString(),
                CustomMessage = customMessage,
                AspectName = aspectName,
                Parameters = logParameters
            };

            return logDetail;
        }
        public static void InformationLog(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message));
        }

        public static void InformationWithExceptionLog(this ILogger logger, string message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message, exception));
        }
        public static void DebugLog(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, message));
        }

        public static void DebugWithExceptionLog(this ILogger logger, string message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, message, exception));
        }
        public static void WarningLog(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, message));
        }

        public static void WarningWithExceptionLog(this ILogger logger, string message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, message, exception));
        }
        public static void ErrorLog(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, message));
        }

        public static void ErrorWithExceptionLog(this ILogger logger, string message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, message, exception));
        }
        public static void FatalLog(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, message));
        }

        public static void FatalWithExceptionLog(this ILogger logger, string message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, message, exception));
        }
        public static void TraceLog(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Trace, message));
        }

        public static void TraceWithExceptionLog(this ILogger logger, string message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Trace, message, exception));
        }
    }
}
