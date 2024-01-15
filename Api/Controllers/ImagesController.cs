using Croptor.Api.Attributes;
using Croptor.Api.ViewModels.Image;
using Croptor.Application.Images.Queries.CropImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO.Compression;

namespace Croptor.Api.Controllers;

[Route("images")]
[ApiController]
public class ImagesController( /*IMapper mapper,*/ IMediator mediator, IHostEnvironment environment) : ControllerBase
{
    [HttpPost("crop")]
    [MaxFilesAuthorization(3)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> Crop(
        [FromForm] List<IFormFile> files,
        [FromForm] string images
    )
    {
        var imagesParams = JsonConvert.DeserializeObject<ImagesParamsDto>(images);
        if (imagesParams == null) throw new ArgumentException("images must be a valid json");

        var guid = Guid.NewGuid();
        var path = Path.Combine(environment.ContentRootPath, "wwwroot", guid.ToString());
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        foreach (var file in files)
        {
            if (!file.ContentType.StartsWith("image/")) continue;
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await mediator.Send(new CropImageQuery(
                memoryStream,
                file.FileName,
                imagesParams.Params[file.FileName],
                imagesParams.Sizes,
                path
            ));
        }

        var newGuid = Guid.NewGuid();
        var archiveDir = Path.Combine(environment.ContentRootPath, "wwwroot", newGuid.ToString());
        var archivePath = Path.Combine(archiveDir, "cropped.zip");
        if (!Directory.Exists(archiveDir)) Directory.CreateDirectory(archiveDir);
        ZipFile.CreateFromDirectory(path, archivePath);
        var uri = $"{Request.Scheme}://{Request.Host}/images/download/{newGuid}/cropped.zip";
        return Created(uri, uri);
    }

    [HttpGet("download/{id:guid}/cropped.zip")]
    public async Task<ActionResult<string>> Download(Guid id)
    {
        var fileStream =
            new FileStream(Path.Combine(environment.ContentRootPath, "wwwroot", id.ToString(), "cropped.zip"),
                FileMode.Open);
        return File(fileStream, "application/zip");
    }
}