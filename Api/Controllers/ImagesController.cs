using Croptor.Application.Orders.Queries.CreateWayForPay.Image;
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
            if (!file.ContentType.StartsWith("image/"))
                continue;
            using MemoryStream memoryStream = new MemoryStream();
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
        var archiveDir = Path.Combine(environment.ContentRootPath, "wwwroot/archives", newGuid.ToString());
        var archivePath = Path.Combine(archiveDir, "cropped.zip");
        if (!Directory.Exists(archiveDir)) Directory.CreateDirectory(archiveDir);
        ZipFile.CreateFromDirectory(path, archivePath);
        Directory.Delete(path);
        var uri = $"{Request.Scheme}://{Request.Host}/images/download/{newGuid}/cropped.zip";
        return Created(uri, uri);
    }

    [HttpGet("download/{id:guid}/cropped.zip")]
    public async Task<ActionResult> Download(Guid id)
    {
        var fileStream =
            new FileStream(Path.Combine(environment.ContentRootPath, "wwwroot/archives", id.ToString(), "cropped.zip"),
                FileMode.Open);
        return File(fileStream, "application/zip");
    }

    [HttpGet("get/{path}")]
    public async Task<ActionResult> Get(string path)
    {
        var fileStream =
            new FileStream(Path.Combine(environment.ContentRootPath, "wwwroot/images", path),
                FileMode.Open);
        return File(fileStream, "application/zip");
    }
    [HttpPost("upload/{path}")]
    public async Task<ActionResult<string>> upload([FromForm] List<IFormFile> files)
    {
        var file = files[0];
        if (!file.ContentType.StartsWith("image/")) return BadRequest("File is not an image");
        string newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var fileStream =
            new FileStream(Path.Combine(environment.ContentRootPath, "wwwroot/images", newFileName),
                FileMode.CreateNew);
        await file.CopyToAsync(fileStream);
        var uri = $"{Request.Scheme}://{Request.Host}/images/get/"+newFileName;
        return Created(uri, uri);
    }
}