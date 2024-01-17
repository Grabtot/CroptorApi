using Croptor.Domain.Users;
using MediatR;

namespace Croptor.Application.Users.Queries.GetUser;

public record GetUserQuery() : IRequest<User>;