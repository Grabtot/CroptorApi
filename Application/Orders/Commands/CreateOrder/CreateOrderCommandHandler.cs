using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Commands;

public class CreateOrderCommandHandler(IUserProvider userProvider,
    IOrderRepository orderRepository
    ) : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (userProvider.UserId is null)
        {
            throw new UserNotAuthenticatedException();
        }

        Guid userId = userProvider.UserId.Value;
        Order order = Order.Create(userId, request.Amount);
        
        await orderRepository.AddAsync(order, cancellationToken);

        return order;
    }
}