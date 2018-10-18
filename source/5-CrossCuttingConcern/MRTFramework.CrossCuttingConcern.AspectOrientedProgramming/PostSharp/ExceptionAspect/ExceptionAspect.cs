using System;
using System.Reflection;
using MRTFramework.CrossCuttingConcern.Logging.Abstract;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;
using MRTFramework.Model.Enums;
using PostSharp.Aspects;

namespace MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.ExceptionAspect
{
    [Serializable]
    public class ExceptionAspect : OnExceptionAspect
    {
        private readonly Type _loggerServiceType;
        private readonly Type _loggerProcessType;
        private ILogger _logService;

        public ExceptionAspect(Type loggerServiceType, Type loggerProcessType)
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

        public override void OnException(MethodExecutionArgs args)
        {
            if (!_logService.IsEnabledFor(LoggingEventType.Error))
            {
                return;
            }

            var aspectName = this.GetType().Name;
            var jsonLogDetail = args.LogMethodDetail(null, aspectName).ToJson();

            _logService.ErrorWithExceptionLog(jsonLogDetail, args.Exception);
        }
    }
}
