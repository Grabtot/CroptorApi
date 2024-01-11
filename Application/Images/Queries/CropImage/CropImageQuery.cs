using Croptor.Api.ViewModels.Image;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Images.Queries.CropImage;

public record CropImageQuery(
    MemoryStream MemoryStream,
    string FileName,
    ImageParamsDto ImageParams,
    List<CropSizeDto> Sizes 
) : IRequest<string>;