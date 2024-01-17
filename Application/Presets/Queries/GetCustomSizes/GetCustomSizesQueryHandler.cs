using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Constants;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries.GetCustomSizes;

public class GetCustomSizesQueryHandler(
    IUserProvider userProvider,
    IPresetRepository presetRepository
    ) : IRequestHandler<GetCustomSizesQuery, Preset>
{
    public async Task<Preset> Handle(GetCustomSizesQuery request, CancellationToken cancellationToken)
    {
        if (userProvider.UserId is null)
        {
            throw new UserNotAuthenticatedException();
        }

        Guid userId = userProvider.UserId.Value;

        Preset? preset = await presetRepository.GetCustomSizes(userId, cancellationToken) ??
            Preset.Create(Constants.Presets.CustomName, userId, Constants.Presets.DefaultCustomIconUri);

        return preset;
    }
}