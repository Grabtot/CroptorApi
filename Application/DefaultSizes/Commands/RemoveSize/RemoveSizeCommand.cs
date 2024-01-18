using Croptor.Domain.Common.ValueObjects;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.RemoveSize;

public record RemoveSizeCommand(Guid CategoryId, Size Size) : IRequest;