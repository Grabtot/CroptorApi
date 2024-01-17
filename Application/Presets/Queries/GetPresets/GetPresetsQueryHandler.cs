using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Croptor.Application.Presets.Queries;

public class GetPresetsQueryHandler(
    IUserProvider userProvider,
    IPresetRepository presetRepository) : IRequestHandler<GetPresetsQuery, List<Guid>>
{

    private readonly IPresetRepository _presetRepository = presetRepository;
    private readonly IUserProvider _userProvider = userProvider;

    public async Task<List<Guid>> Handle(GetPresetsQuery request, CancellationToken cancellationToken)
    {
        if (_userProvider.UserId is null)
        {
            return [];
        }

        Guid userId = _userProvider.UserId.Value;

        return await _presetRepository.GetUserPresetIds(userId, cancellationToken);
    }
}