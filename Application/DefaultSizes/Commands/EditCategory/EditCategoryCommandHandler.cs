using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.EditCategory;

public class EditCategoryCommandHandler(IPresetRepository repository): IRequestHandler<EditCategoryCommand>
{
    public async Task Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        Preset preset = await repository.GetAsync(request.Id, cancellationToken);
        preset.Name = request.Name;
        preset.IconUri = request.Icon ?? preset.IconUri ??
                         new Uri("https://croptor.com/images/get/icons/customSize.svg");
    }
}