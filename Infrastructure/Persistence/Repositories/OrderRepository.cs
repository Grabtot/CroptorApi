using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Croptor.Infrastructure.Persistence.Repositories;

public class OrderRepository(CroptorDbContext context) : IOrderRepository
{
    private readonly DbSet<Order> _dbSet = context.Set<Order>();
    
}