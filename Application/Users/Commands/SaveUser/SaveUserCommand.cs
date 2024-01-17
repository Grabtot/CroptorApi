using MediatR;

namespace Croptor.Application.Users.Commands.SaveUser;

public record SaveUserCommand(SaveUserDto User) : IRequest;