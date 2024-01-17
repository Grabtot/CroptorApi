using Croptor.Application.Common.Validation;
using FluentValidation;

namespace Croptor.Application.Presets.Commands.AddCustomSize
{
    public class AddCustomSizeCommandValidator : AbstractValidator<AddCustomSizeCommand>
    {
        public AddCustomSizeCommandValidator(SizeValidator sizeValidator)
        {
            RuleFor(size => size.Size).SetValidator(sizeValidator);
        }
    }
}
