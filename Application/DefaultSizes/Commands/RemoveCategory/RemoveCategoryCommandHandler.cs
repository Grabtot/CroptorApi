using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.RemoveCategory;

public class RemoveCategoryCommandHandler(IPresetRepository repository): IRequestHandler<RemoveCategoryCommand>
{
    public async Task Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        Preset preset = await repository.GetAsync(request.Id, cancellationToken);
        repository.Delete(preset);
    }
}