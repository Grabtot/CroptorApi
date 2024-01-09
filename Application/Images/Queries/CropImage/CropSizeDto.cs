namespace Croptor.Api.ViewModels.Image;

public record CropSizeDto(
    int Width,
    int Height,
    string? Name
);