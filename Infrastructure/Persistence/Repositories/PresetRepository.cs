using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Presets;
using Microsoft.EntityFrameworkCore;

namespace Croptor.Infrastructure.Persistence.Repositories
{
    public class PresetRepository(CroptorDbContext context) : IPresetRepository
    {
        private readonly DbSet<Preset> _dbSet = context.Set<Preset>();

        public async Task AddAsync(Preset preset, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(preset, cancellationToken);
        }

        public async Task<Preset> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync([id], cancellationToken)
                ?? throw new InvalidOperationException($"Could not find {id}");
        }
    }
}
