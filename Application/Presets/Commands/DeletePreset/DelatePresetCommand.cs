using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.DeletePreset
{
    public record DelatePresetCommand(Guid Id) : IRequest<Preset>
    {
    }
}
