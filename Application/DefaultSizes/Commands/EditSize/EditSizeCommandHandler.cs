using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Application.DefaultSizes.Commands.EditCategory;
using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.EditSize;

public class EditSizeCommandHandler(IPresetRepository repository): IRequestHandler<EditSizeCommand>
{
    public async Task Handle(EditSizeCommand request, CancellationToken cancellationToken)
    {
        Preset preset = await repository.GetAsync(request.CategoryId, cancellationToken);
        int index = preset.Sizes.FindIndex(size => size == request.OldSize);
        if (index >= 0)
        {
            preset.Sizes[index] = request.NewSize;
        }
    }
}