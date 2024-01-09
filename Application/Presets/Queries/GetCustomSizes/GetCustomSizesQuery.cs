using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries.GetCustomSizes;

public record GetCustomSizesQuery() : IRequest<Preset>;