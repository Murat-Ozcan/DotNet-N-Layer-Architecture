using System;
using System.Reflection;
using MRTFramework.CrossCuttingConcern.Logging.Abstract;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;
using MRTFramework.Model.Enums;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.LogAspect
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    public class LogAspect : OnMethodBoundaryAspect
    {
        private readonly Type _loggerServiceType;
        private readonly Type _loggerProcessType;
        private ILogger _logService;

        public LogAspect(Type loggerServiceType, Type loggerProcessType)
        {
            _loggerServiceType = loggerServiceType;
            _loggerProcessType = loggerProcessType;
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

            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {

            if (!_logService.IsEnabledFor(LoggingEventType.Information))
            {
                return;
            }

            var aspectName = this.GetType().Name;
            var jsonLogDetail = args.LogMethodDetail(null, aspectName).ToJson();

            _logService.InformationLog(jsonLogDetail);
        }
    }
}
