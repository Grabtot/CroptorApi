using Croptor.Domain.Presets;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IPresetRepository
    {
        Task AddAsync(Preset preset, CancellationToken cancellationToken);
        Task<Preset> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}