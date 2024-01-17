using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Commands.ApproveOrder;

public record ApproveOrderCommand(Order Order) : IRequest;