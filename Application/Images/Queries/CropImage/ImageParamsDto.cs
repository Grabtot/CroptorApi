namespace Croptor.Api.ViewModels.Image;

public record ImageParamsDto(
    string VerticalSnap,
    string HorizontalSnap,
    CenterDto? Center,
    bool FitNCrop
    );