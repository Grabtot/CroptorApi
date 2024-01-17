using System;
using System.Collections.Generic;

namespace Croptor.Application.Orders.Queries.CreateWayForPay.Image;

public record ImagesParamsDto(
    List<CropSizeDto> Sizes,
    Dictionary<string, ImageParamsDto> Params
);