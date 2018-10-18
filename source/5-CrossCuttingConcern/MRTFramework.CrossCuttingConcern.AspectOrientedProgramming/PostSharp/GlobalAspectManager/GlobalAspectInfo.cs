using MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.ExceptionAspect;
using MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.PerformanceAspect;
using MRTFramework.CrossCuttingConcern.Logging.Concrete.NLog;

//[assembly: ExceptionAspect(loggerServiceType: typeof(NLogService), loggerProcessType: typeof(NLogDatabaseLogger), AttributeTargetTypes = "MRTFramework.Business.Domain.Managers.*")]
//[assembly: LogAspect(loggerServiceType: typeof(NLogService), loggerProcessType: typeof(NLogDatabaseLogger), AttributeTargetTypes = "MRTFramework.Business.Domain.Managers.*")]
//[assembly: PerformanceAspect(loggerServiceType: typeof(NLogService), loggerProcessType: typeof(NLogDatabaseLogger), interval: 0, AttributeTargetTypes = "MRTFramework.Business.Domain.Managers.*")]
[assembly: PerformanceAspect(loggerServiceType: typeof(NLogService), loggerProcessType: typeof(NLogDatabaseLogger), AttributeTargetTypes = "MRTFramework.Business.Domain.Managers.*")]


