using FluentValidation;

namespace MRTFramework.CrossCuttingConcern.Validation.FluentValidation
{
    public class FluentValidatorTool
    {
        public static void FluentValidate(IValidator validator, object entity)
        {
            var result = validator.Validate(entity);
            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
