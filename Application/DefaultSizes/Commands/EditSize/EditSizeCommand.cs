using Croptor.Domain.Common.ValueObjects;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.EditSize;

public record EditSizeCommand(Guid CategoryId, Size OldSize, Size NewSize): IRequest;