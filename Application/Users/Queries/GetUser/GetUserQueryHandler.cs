using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Users;
using MediatR;

namespace Croptor.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(
    IUserProvider userProvider,
    IUserRepository userRepository
) : IRequestHandler<GetUserQuery, User>
{
    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        if (userProvider.UserId is null)
        {
            throw new UserNotAuthenticatedException();
        }

        Guid userId = userProvider.UserId.Value;
        User user = await userRepository.GetUserAsync(userId, cancellationToken);

        userRepository.UpdateSubscriptionForUser(user);

        return user;
    }
}