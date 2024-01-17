using Croptor.Application.Common.Validation;
using FluentValidation;

namespace Croptor.Application.Presets.Commands.AddCustomSize
{
    public class RemoveCustomSizeCommandValidator : AbstractValidator<AddCustomSizeCommand>
    {
        public RemoveCustomSizeCommandValidator(SizeValidator sizeValidator)
        {
            RuleFor(size => size.Size).SetValidator(sizeValidator);
        }
    }
}
