namespace Croptor.Api.ViewModels.User;

public record UserDto(
    string Name,
    string Email,
    string? Image,
    string Plan,
    DateTime Expires
    );