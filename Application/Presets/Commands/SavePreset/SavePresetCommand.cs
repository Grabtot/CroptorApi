using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.SavePreset;

public record SavePresetCommand(Preset Preset) : IRequest;