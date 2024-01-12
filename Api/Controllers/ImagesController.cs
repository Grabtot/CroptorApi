using Croptor.Api.Attributes;
using Croptor.Api.ViewModels.Image;
using Croptor.Application.Images.Queries.CropImage;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Croptor.Api.Controllers;

[Route("images")]
[ApiController]
public class ImagesController(/*IMapper mapper,*/ IMediator mediator) : ControllerBase
{
    [HttpPost("crop")]
    [MaxFilesAuthorization(3)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> Crop([FromForm] List<IFormFile> files, [FromForm] ImagesParamsDto imagesParams)
    {
        foreach (var file in files)
        {
            if (!file.ContentType.StartsWith("image/")) continue;
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await mediator.Send(new CropImageQuery(
                memoryStream,
                file.FileName,
                imagesParams.Images[file.FileName],
                imagesParams.Sizes
            ));
        }

        return Ok("Kinda link to archive"); //TODO
    }
}