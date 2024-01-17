namespace Croptor.Application.Orders.Queries.CreateWayForPay.Preset;

public record PresetDto(
    Guid? Id,
    string Name,
    List<Domain.Common.ValueObjects.Size> Sizes
);