using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.AddCategory;

public class AddCategoryCommandHandler(IPresetRepository repository) : IRequestHandler<AddCategoryCommand>
{
    public Task Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var preset = Preset.Create(
            request.Name,
            request.Icon ??
            new Uri("https://croptor.com/images/get/icons/custom-size.svg")
        );

        return repository.AddAsync(preset, cancellationToken);
    }
}