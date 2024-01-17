using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using Croptor.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Croptor.Infrastructure.Persistence.Repositories
{
    public class UserRepository(CroptorDbContext context) : IUserRepository
    {
        private readonly DbSet<User> _dbSet = context.Set<User>();
        public async Task<List<Preset>> GetPresetsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return (await _dbSet.Where(user => user.Id == userId)
                    .Include(user => user.Presets)
                    .FirstAsync(cancellationToken))
                .Presets;
        }

        public async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([userId], cancellationToken)
                   ?? throw new InvalidOperationException($"Could not find User {userId}");
        }
    }
}