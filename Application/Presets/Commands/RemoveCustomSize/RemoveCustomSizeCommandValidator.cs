using FluentValidation;

namespace Croptor.Application.Presets.Commands.AddCustomSize
{
    public class RemoveCustomSizeCommandValidator : AbstractValidator<AddCustomSizeCommand>
    {
        public RemoveCustomSizeCommandValidator()
        {
            RuleFor(size => size.Size.Width).GreaterThan(0);
            RuleFor(size => size.Size.Height).GreaterThan(0);
        }
    }
}
