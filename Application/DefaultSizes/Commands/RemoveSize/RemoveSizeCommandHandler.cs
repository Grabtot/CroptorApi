using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.RemoveSize;

public class RemoveSizeCommandHandler(IPresetRepository repository) : IRequestHandler<RemoveSizeCommand>
{
    public async Task Handle(RemoveSizeCommand request, CancellationToken cancellationToken)
    {
        Preset preset = await repository.GetAsync(request.CategoryId, cancellationToken);
        preset.RemoveSize(request.Size);
    }
}