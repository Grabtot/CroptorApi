using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.SavePreset;

public class SavePresetCommandHandler(
    IPresetRepository presetRepository
) : IRequestHandler<SavePresetCommand>
{
    public async Task Handle(SavePresetCommand request, CancellationToken cancellationToken)
    {
        Preset? preset = await presetRepository.GetOrDefaultAsync(request.Preset.Id, cancellationToken);
        if (preset != null)
            await presetRepository.UpdateAsync(request.Preset, cancellationToken);
        else
            await presetRepository.AddAsync(request.Preset, cancellationToken);
    }
}