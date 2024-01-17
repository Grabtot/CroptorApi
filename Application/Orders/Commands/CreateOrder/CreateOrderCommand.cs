using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Commands;

public record CreateOrderCommand(
    int Amount
    ): IRequest<Order>;