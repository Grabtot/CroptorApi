namespace Croptor.Api.ViewModels.Preset;

public record PresetDto(
    Guid? Id,
    string Name,
    List<Domain.Common.ValueObjects.Size> Sizes
);