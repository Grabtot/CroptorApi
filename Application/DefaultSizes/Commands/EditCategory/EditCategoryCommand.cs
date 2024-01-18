using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.EditCategory;

public record EditCategoryCommand(Guid Id, string Name, Uri? Icon) : IRequest;