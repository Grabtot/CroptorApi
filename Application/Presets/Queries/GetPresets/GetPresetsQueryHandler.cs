using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Queries;

public class GetPresetsQueryHandler(
    IUserProvider userProvider,
    IUserRepository userRepository
    
) : IRequestHandler<GetPresetsQuery, List<Guid>>
{
    public async Task<List<Guid>> Handle(GetPresetsQuery request, CancellationToken cancellationToken)
    {
        if (userProvider.UserId is null)
        {
            throw new UserNotAuthenticatedException();
        }

        Guid userId = userProvider.UserId.Value;

        List<Preset> presets = await userRepository.GetPresets(userId, cancellationToken);

        return presets.Select(preset => preset.Id).ToList();
    }
}