namespace Croptor.Application.Orders.Queries.CreateWayForPay.User;

public record UserDto(
    string Name,
    string Email,
    string? Image,
    string Plan,
    DateOnly Expires
    );