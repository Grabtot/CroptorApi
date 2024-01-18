using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Queries.GetCategories;

public record GetCategoriesQuery() : IRequest<List<Preset>>;