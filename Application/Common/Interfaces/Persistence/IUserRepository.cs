using Croptor.Domain.Presets;
using Croptor.Domain.Users;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Guid?> TryGetCustomSizesIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<Preset>> GetPresetsAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}