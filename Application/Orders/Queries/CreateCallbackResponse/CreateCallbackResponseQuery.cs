using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Queries.CreateCallbackResponse;

public record CreateCallbackResponseQuery(Order Order, string SecretKey) : IRequest<WayForPayCallbackResponse>;