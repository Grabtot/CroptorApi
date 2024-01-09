using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries.GetPreset;

public class GetPresetQueryHandler(
    IPresetRepository presetRepository
) : IRequestHandler<GetPresetQuery, Preset>
{
    public Task<Preset> Handle(GetPresetQuery request, CancellationToken cancellationToken)
    {
        return presetRepository.GetAsync(request.Id, cancellationToken);
    }
}