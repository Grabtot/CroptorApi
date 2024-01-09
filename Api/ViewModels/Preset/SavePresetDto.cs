namespace Croptor.Api.ViewModels.Preset;

public record SavePresetDto(
    Guid? Id,
    string Name,
    List<Domain.Common.ValueObjects.Size> Sizes
);