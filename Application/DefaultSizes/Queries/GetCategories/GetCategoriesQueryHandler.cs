using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Queries.GetCategories;

public class GetCategoriesQueryHandler(IPresetRepository repository): IRequestHandler<GetCategoriesQuery, List<Preset>>
{
    public Task<List<Preset>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}