﻿using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Constants;
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

        public async Task<Preset?> GetCustomSizes(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(preset => preset.UserId == userId && preset.Name == Constants.Presets.CustomName)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Preset?> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public Task UpdateAsync(Preset preset, CancellationToken cancellationToken)
        {
            _dbSet.Update(preset);
            return Task.CompletedTask;
        }

        public async Task<List<Preset>> GetUserPresets(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(preset => preset.UserId == userId
                  && (preset.Name != Constants.Presets.CustomName))
                  .ToListAsync(cancellationToken);
        }
        public async Task<List<Guid>> GetUserPresetIds(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(preset => preset.UserId == userId
                    && (preset.Name != Constants.Presets.CustomName))
                .Select(preset => preset.Id)
                .ToListAsync(cancellationToken);
        }

        public void Delete(Preset preset) => _dbSet.Remove(preset);

        public async Task<List<Preset>> GetDefaultPresetsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(p => p.UserId == null).ToListAsync(cancellationToken);
        }
    }
}