using Croptor.Domain.Common.ValueObjects;
using MediatR;

namespace Croptor.Application.DefaultSizes.Commands.AddSize;

public record AddSizeCommand(Guid СategoryId, Size Size) : IRequest;