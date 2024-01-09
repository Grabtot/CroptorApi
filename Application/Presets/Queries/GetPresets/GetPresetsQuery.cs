using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries;

public record GetPresetsQuery(): IRequest<List<Guid>>;