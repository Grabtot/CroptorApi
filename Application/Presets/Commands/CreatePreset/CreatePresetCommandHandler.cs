using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.CreatePreset
{
    public class CreatePresetCommandHandler(IUserProvider userProvider,
        IPresetRepository presetRepository) : IRequestHandler<CreatePresetCommand, Preset>
    {
        private readonly IUserProvider _userProvider = userProvider;
        private readonly IPresetRepository _presetRepository = presetRepository;

        public async Task<Preset> Handle(CreatePresetCommand command, CancellationToken cancellationToken)
        {
            if (_userProvider.UserId is null)
            {
                throw new UserNotAuthenticatedException();
            }

            Preset preset = Preset.Create(
                command.Name,
                _userProvider.UserId.Value,
                command.Sizes,
                null);

            await _presetRepository.AddAsync(preset, cancellationToken);

            return preset;
        }
    }
}
