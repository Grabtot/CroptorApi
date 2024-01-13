using Croptor.Domain.Users;
using MediatR;

namespace Croptor.Application.Users.Queries;

public record GetUserQuery() : IRequest<User>;