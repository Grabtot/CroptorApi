using Croptor.Domain.Users;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
        void UpdateSubscriptionForUser(User user);
    }
}