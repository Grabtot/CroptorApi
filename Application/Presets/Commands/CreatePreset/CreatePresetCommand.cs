using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.CreatePreset
{
    public record CreatePresetCommand(string Name, List<Size> Sizes) : IRequest<Preset>
    {
    }
}
