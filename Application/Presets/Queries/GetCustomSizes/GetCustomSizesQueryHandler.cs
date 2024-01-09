using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Constants;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries.GetCustomSizes;

public class GetCustomSizesQueryHandler(
    IUserProvider userProvider,
    IUserRepository userRepository,
    IPresetRepository presetRepository
    ): IRequestHandler<GetCustomSizesQuery, Preset>
{
    public async Task<Preset> Handle(GetCustomSizesQuery request, CancellationToken cancellationToken)
    {
        if (userProvider.UserId is null)
        {
            throw new UserNotAuthenticatedException();
        }

        Guid userId = userProvider.UserId.Value;
        
        Guid? presetId = await userRepository
            .TryGetCustomSizesIdAsync(userId, cancellationToken);
        
        Preset preset;
        if (presetId is not null)
        {
            preset = await presetRepository.GetAsync(presetId.Value, cancellationToken);
        }
        else
        {
            preset = Preset.Create(Constants.Presets.CustomName,
                userId,
                Constants.Presets.DefaultCustomIconUri);

            await presetRepository.AddAsync(preset, cancellationToken);
        }

        return preset;
    }
}