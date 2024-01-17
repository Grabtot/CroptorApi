using Croptor.Application.Orders.Queries.CreateWayForPay;
using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Queries.CreateRequest;

public record CreateRequestQuery(
    Order Order,
    string Account,
    string SecretKey
    ) : IRequest<WayForPayRequest>;