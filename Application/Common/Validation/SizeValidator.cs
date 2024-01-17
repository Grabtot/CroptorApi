using Croptor.Domain.Common.ValueObjects;
using FluentValidation;

namespace Croptor.Application.Common.Validation
{
    public class SizeValidator : AbstractValidator<Size>
    {
        public SizeValidator()
        {
            RuleFor(size => size.Width).GreaterThan(0);
            RuleFor(size => size.Height).GreaterThan(0);
        }
    }
}
