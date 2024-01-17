using Croptor.Application.Common.Validation;
using Croptor.Domain.Common.Constants;
using FluentValidation;

namespace Croptor.Application.Presets.Commands.CreatePreset
{
    public class CreatePresetCommandValidator : AbstractValidator<CreatePresetCommand>
    {
        public CreatePresetCommandValidator(SizeValidator sizeValidator)
        {
            RuleFor(command => command.Name)
                .NotEqual(Constants.Presets.CustomName)
                .NotNull()
                .NotEmpty();

            RuleForEach(command => command.Sizes)
                .SetValidator(sizeValidator);
        }
    }
}
