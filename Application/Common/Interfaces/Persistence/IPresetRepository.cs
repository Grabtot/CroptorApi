using Croptor.Domain.Presets;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IPresetRepository
    {
        Task AddAsync(Preset preset, CancellationToken cancellationToken = default);
        Task<Preset> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Preset?> GetOrDefaultAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Preset preset, CancellationToken cancellationToken = default);
    }
}