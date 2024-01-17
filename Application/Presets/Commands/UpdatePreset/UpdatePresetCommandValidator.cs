using Croptor.Application.Common.Validation;
using Croptor.Domain.Common.Constants;
using FluentValidation;

namespace Croptor.Application.Presets.Commands.UpdatePreset
{
    public class UpdatePresetCommandValidator : AbstractValidator<UpdatePresetCommand>
    {
        public UpdatePresetCommandValidator(SizeValidator sizeValidator)
        {
            RuleFor(command => command.Name)
                .NotEqual(Constants.Presets.CustomName)
                .NotNull()
                .NotEmpty();

            RuleForEach(command => command.Sizes)
                .SetValidator(sizeValidator);

            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
