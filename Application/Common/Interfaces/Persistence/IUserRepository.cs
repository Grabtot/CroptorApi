using Croptor.Domain.Users;

namespace Croptor.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetUser(Guid userId, CancellationToken cancellationToken = default);
    }
}