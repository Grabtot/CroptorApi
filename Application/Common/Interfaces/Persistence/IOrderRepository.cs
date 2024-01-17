using Croptor.Domain.Users.ValueObjects;

namespace Croptor.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);
}