
using Croptor.Domain.Presets;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Guid?> TryGetCustomSizesIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<Preset>> GetPresets(Guid userId, CancellationToken cancellationToken = default);
    }
}