using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.AddCategory;

public class AddCategoryCommandHandler(IPresetRepository repository) : IRequestHandler<AddCategoryCommand, Guid>
{
    public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var preset = Preset.Create(
            request.Name,
            request.Icon ??
            new Uri("https://croptor.com/images/get/icons/customSize.svg")
        );

         await repository.AddAsync(preset, cancellationToken);
         return preset.Id;
    }
}