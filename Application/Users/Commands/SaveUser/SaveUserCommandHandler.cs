using Croptor.Application.Common.Interfaces;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Common.Exceptions;
using Croptor.Domain.Users;
using MediatR;

namespace Croptor.Application.Users.Commands.SaveUser;

public class SaveUserCommandHandler(
    IUserRepository userRepository, 
    IUserProvider userProvider
    ) : IRequestHandler<SaveUserCommand>
{
    public async Task Handle(SaveUserCommand request, CancellationToken cancellationToken)
    {
        if (userProvider.UserId is null)
        {
            throw new UserNotAuthenticatedException();
        }

        Guid userId = userProvider.UserId.Value;
        User user = await userRepository.GetUserAsync(userId, cancellationToken);
        user.UserName = request.User.Name;
        // user.Email = request.User.Email;
        user.Image = request.User.Image;
    }
}