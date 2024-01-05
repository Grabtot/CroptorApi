
namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Guid> TryGetCustomSizesIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}