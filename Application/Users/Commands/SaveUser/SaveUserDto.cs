namespace Croptor.Application.Users.Commands.SaveUser;

public record SaveUserDto(
    string Name,
    // string Email,
    string? Image
);