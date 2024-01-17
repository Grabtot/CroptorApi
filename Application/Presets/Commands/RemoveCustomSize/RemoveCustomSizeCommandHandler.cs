using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.AddCustomSize
{
    public class RemoveCustomSizeCommandHandler(IUserProvider userProvider,
        IPresetRepository presetProvider) : IRequestHandler<RemoveCustomSizeCommand, Preset?>
    {
        private readonly IUserProvider _userProvider = userProvider;
        private readonly IPresetRepository _presetRepository = presetProvider;

        public async Task<Preset?> Handle(RemoveCustomSizeCommand command, CancellationToken cancellationToken)
        {
            if (_userProvider.UserId is null)
            {
                throw new UserNotAuthenticatedException();
            }

            Guid userId = _userProvider.UserId.Value;

            Preset? preset = await _presetRepository.GetCustomSizes(userId, cancellationToken);
            if (preset is not null)
            {
                Size size = command.Size;
                size.IconUri = preset.IconUri;

                preset.RemoveSize(size);
            }

            return preset;
        }
    }
}
