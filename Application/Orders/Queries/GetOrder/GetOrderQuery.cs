using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Queries;

public record GetOrderQuery(Guid Id): IRequest<Order>;