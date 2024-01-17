using System;
using System.Collections.Generic;
using Croptor.Application.Orders.Queries.CreateWayForPay.Image;

namespace Croptor.Api.ViewModels.Image;

public record ImagesParamsDto(
    List<CropSizeDto> Sizes,
    Dictionary<string, ImageParamsDto> Params
);