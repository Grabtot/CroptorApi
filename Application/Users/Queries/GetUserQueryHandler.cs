using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using MediatR;
using Croptor.Domain.Users;

namespace Croptor.Application.Users.Queries;

public class GetUserQueryHandler(
    IUserProvider userProvider,
    IUserRepository userRepository
) : IRequestHandler<GetUserQuery, User>
{
    public Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        if (userProvider.UserId is null)
        {
            throw new UserNotAuthenticatedException();
        }

        Guid userId = userProvider.UserId.Value;
        return userRepository.GetUser(userId, cancellationToken);
    }
}