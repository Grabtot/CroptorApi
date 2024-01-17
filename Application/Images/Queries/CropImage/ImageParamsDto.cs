namespace Croptor.Application.Orders.Queries.CreateWayForPay.Image;

public record ImageParamsDto(
    string VerticalSnap,
    string HorizontalSnap,
    CenterDto? Center,
    bool FitNCrop
    );