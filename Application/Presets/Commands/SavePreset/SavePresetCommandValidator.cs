using Croptor.Domain.Common.Constants;
using FluentValidation;

namespace Croptor.Application.Presets.Commands.SavePreset
{
    public class SavePresetCommandValidator : AbstractValidator<SavePresetCommand>
    {
        public SavePresetCommandValidator()
        {
            RuleFor(command => command.Preset.Name)
                .NotEqual(Constants.Presets.CustomName)
                .NotNull()
                .NotEmpty();
        }
    }
}
