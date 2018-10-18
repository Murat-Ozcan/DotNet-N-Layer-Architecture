using System;
using System.Reflection;
using MRTFramework.CrossCuttingConcern.Logging.Abstract;
using MRTFramework.CrossCuttingConcern.Performance.Concrete;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;
using MRTFramework.Model.Enums;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.PerformanceAspect
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    public class PerformanceAspect : OnMethodBoundaryAspect
    {
        private readonly uint _interval;
        [NonSerialized] private PerformanceTimer _performanceTimer;
        private readonly Type _loggerServiceType;
        private readonly Type _loggerProcessType;
        private readonly PerformanceType _performanceType;
        private ILogger _logService;

        public PerformanceAspect(Type loggerServiceType, Type loggerProcessType)
        {
            _loggerServiceType = loggerServiceType;
            _loggerProcessType = loggerProcessType;
            _performanceType = PerformanceType.Info;
        }

        public PerformanceAspect(Type loggerServiceType, Type loggerProcessType, uint interval)
        {
            _loggerServiceType = loggerServiceType;
            _loggerProcessType = loggerProcessType;
            _interval = interval;
            _performanceType = PerformanceType.Warning;
        }

        public override void RuntimeInitialize(MethodBase method)
        {

            if (typeof(ILogger).IsAssignableFrom(_loggerServiceType) == false)
            {
                throw new Exception("Wrong Log Service Manager");
            }

            if (_loggerProcessType.BaseType != _loggerServiceType)
            {
                throw new Exception("Wrong Logger Process Type");
            }

            _logService = (ILogger)Activator.CreateInstance(_loggerProcessType);
            _performanceTimer = Activator.CreateInstance<PerformanceTimer>();

            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _performanceTimer.StartTime();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _performanceTimer.StopTime();

            var totalTime = _performanceTimer.ElapsedTime();
            var aspectName = this.GetType().Name;
            string message;
            string jsonLogDetail;

            switch (_performanceType)
            {
                case PerformanceType.Info:

                    if (!_logService.IsEnabledFor(LoggingEventType.Information))
                    {
                        return;
                    }

                    message = $"The method runtime took {totalTime} seconds.";

                    jsonLogDetail = args.LogMethodDetail(message, aspectName).ToJson();

                    _logService.InformationLog(jsonLogDetail);
                    break;

                case PerformanceType.Warning:

                    if (!_logService.IsEnabledFor(LoggingEventType.Warning))
                    {
                        return;
                    }

                    if (totalTime > _interval)
                    {
                        message =
                            $"The specified runtime is {_interval} seconds, It took {totalTime} seconds. Method runtime and specified runtime difference {totalTime - _interval} seconds.";
                        jsonLogDetail = args.LogMethodDetail(message, aspectName).ToJson();
                        _logService.WarningLog(jsonLogDetail);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
