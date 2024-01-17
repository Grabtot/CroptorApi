using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Croptor.Infrastructure.Persistence.Repositories;

public class OrderRepository(CroptorDbContext context) : IOrderRepository
{
    private readonly DbSet<Order> _dbSet = context.Set<Order>();
    
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(order, cancellationToken);
    }

    public async Task<Order> GetOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken)
               ?? throw new InvalidOperationException($"Could not find {id}");
    }

    public Task DeleteOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(order);
        return Task.CompletedTask;
    }
}