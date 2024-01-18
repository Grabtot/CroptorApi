using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.RemoveCategory;

public record RemoveCategoryCommand(Guid Id) : IRequest;