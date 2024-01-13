using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries.GetPreset;

public class GetPresetQueryHandler(
    IPresetRepository presetRepository
) : IRequestHandler<GetPresetQuery, Preset>
{
    public async Task<Preset> Handle(GetPresetQuery request, CancellationToken cancellationToken)
    {
        return await presetRepository.GetAsync(request.Id, cancellationToken);
    }
}