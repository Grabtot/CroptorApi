using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.DeletePreset
{
    public class DelatePresetCommandHandler(
        IPresetRepository presetRepository,
        IUserProvider userProvider) : IRequestHandler<DelatePresetCommand, Preset>
    {
        private readonly IPresetRepository _presetRepository = presetRepository;
        private readonly IUserProvider _userProvider = userProvider;

        public async Task<Preset> Handle(DelatePresetCommand command, CancellationToken cancellationToken)
        {
            if (!_userProvider.UserId.HasValue)
            {
                throw new UserNotAuthenticatedException();
            }

            Preset preset = await _presetRepository.GetAsync(command.Id, cancellationToken);

            if (preset.UserId != _userProvider.UserId.Value)
            {
                throw new InvalidOperationException("This is not your preset");
            }

            _presetRepository.Delete(preset);

            return preset;
        }
    }
}
