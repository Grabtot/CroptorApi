using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Queries;

public class GetOrderQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderQuery, Order>
{
    public Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        return orderRepository.GetOrderAsync(request.Id, cancellationToken);
    }
}