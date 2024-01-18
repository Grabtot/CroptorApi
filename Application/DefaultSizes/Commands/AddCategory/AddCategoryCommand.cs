using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.AddCategory;

public record AddCategoryCommand(
    string Name,
    Uri? Icon
    ) : IRequest;