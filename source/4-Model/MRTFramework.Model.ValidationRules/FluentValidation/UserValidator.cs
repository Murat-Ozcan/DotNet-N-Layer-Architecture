using FluentValidation;
using MRTFramework.Model.BaseModels.Concrete;

namespace MRTFramework.Model.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().NotNull();
            RuleFor(x => x.Surname).NotEmpty().NotNull();
            RuleFor(x => x.Age).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
