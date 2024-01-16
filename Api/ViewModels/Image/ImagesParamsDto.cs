using System;
using System.Collections.Generic;

namespace Croptor.Api.ViewModels.Image;

public record ImagesParamsDto(
    List<CropSizeDto> Sizes,
    Dictionary<string, ImageParamsDto> Params
);