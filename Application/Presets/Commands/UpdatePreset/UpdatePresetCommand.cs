using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.UpdatePreset
{
    public record UpdatePresetCommand(Guid Id, string Name, List<Size> Sizes) : IRequest<Preset>
    {
    }
}
