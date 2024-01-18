using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Application.DefaultSizes.Commands.AddCategory;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.AddSize;

public class AddSizeCommandHandler(IPresetRepository repository) : IRequestHandler<AddSizeCommand>
{
    public async Task Handle(AddSizeCommand request, CancellationToken cancellationToken)
    {
        Preset preset = await repository.GetAsync(request.СategoryId, cancellationToken);
        preset.Sizes.Add(request.Size);
    }
}