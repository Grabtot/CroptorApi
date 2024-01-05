using Croptor.Domain.Common.ValueObjects;
using MediatR;

namespace Croptor.Domain.Presets.Events
{
    public record PresetCreated(Preset Preset) : INotification;
    public record SizeAdded(Preset Preset, Size Size) : INotification;
}
