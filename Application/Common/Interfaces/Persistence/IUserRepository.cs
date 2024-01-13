using Croptor.Domain.Presets;
using Croptor.Domain.Users;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Guid?> TryGetCustomSizesIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<Preset>> GetPresets(Guid userId, CancellationToken cancellationToken = default);

        Task<User> GetUser(Guid userId, CancellationToken cancellationToken = default);
    }
}