using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.UpdatePreset
{
    public class UpdatePresetCommandHandler(IPresetRepository presetRepository) : IRequestHandler<UpdatePresetCommand, Preset>
    {
        private readonly IPresetRepository _presetRepository = presetRepository;

        public async Task<Preset> Handle(UpdatePresetCommand command, CancellationToken cancellationToken)
        {
            Preset preset = await _presetRepository.GetAsync(command.Id, cancellationToken);

            preset.Name = command.Name;
            preset.Sizes = command.Sizes;

            return preset;
        }
    }
}
