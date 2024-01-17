using Croptor.Domain.Presets;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IPresetRepository
    {
        Task AddAsync(Preset preset, CancellationToken cancellationToken = default);
        Task<Preset> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Preset?> GetCustomSizes(Guid userId, CancellationToken cancellationToken = default);
        Task<List<Preset>> GetUserPresets(Guid userId, CancellationToken cancellationToken = default);
        Task<Preset?> FindAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Preset preset, CancellationToken cancellationToken = default);
        Task<List<Guid>> GetUserPresetIds(Guid userId, CancellationToken cancellationToken = default);
        void Delate(Preset preset);
    }
}