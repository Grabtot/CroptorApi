using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using Croptor.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Croptor.Infrastructure.Persistence.Repositories
{
    public class UserRepository(CroptorDbContext context) : IUserRepository
    {
        private readonly DbSet<User> _dbSet = context.Set<User>();

        public async Task<Guid?> TryGetCustomSizesIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(user => user.Id == userId)
                .Select(user => user.CustomSizesId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Preset>> GetPresets(Guid userId, CancellationToken cancellationToken = default)
        {
            return (await _dbSet.Where(user => user.Id == userId)
                    .Include(user => user.Presets)
                    .FirstAsync(cancellationToken))
                .Presets;
        }
    }
}