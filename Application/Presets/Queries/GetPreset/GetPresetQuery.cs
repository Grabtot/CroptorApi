using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries.GetPreset;

public record GetPresetQuery(Guid Id) : IRequest<Preset>;