using System;
using System.Linq;
using FluentValidation;
using MRTFramework.CrossCuttingConcern.Validation.FluentValidation;
using PostSharp.Aspects;

namespace MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.ValidationAspect
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private readonly Type _validatorType;

        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            if (_validatorType.BaseType == null) return;

            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            var entities = args.Arguments.Where(x => x.GetType() == entityType);

            foreach (var entity in entities)
            {
                FluentValidatorTool.FluentValidate(validator, entity);
            }
        }
    }
}
