using Croptor.Api.ViewModels.Image;
using Croptor.Application.Images.Queries.CropImage;
using Croptor.Application.Images.Queries.ScaleDownImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO.Compression;

namespace Croptor.Api.Controllers;

[Route("images")]
[ApiController]
[AllowAnonymous]
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
        ImagesParamsDto? imagesParams = JsonConvert.DeserializeObject<ImagesParamsDto>(images);
        if (imagesParams == null)
            throw new ArgumentException("images must be a valid json");

        Guid guid = Guid.NewGuid();
        string path = Path.Combine(environment.ContentRootPath, "wwwroot/images", guid.ToString());

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        foreach (IFormFile file in files)
        {
            if (!file.ContentType.StartsWith("image/"))
                continue;

            using MemoryStream memoryStream = new();
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

        Guid newGuid = Guid.NewGuid();
        string archiveDir = Path.Combine(environment.ContentRootPath, "wwwroot/archives", newGuid.ToString());
        string archivePath = Path.Combine(archiveDir, "cropped.zip");

        if (!Directory.Exists(archiveDir))
            Directory.CreateDirectory(archiveDir);
        ZipFile.CreateFromDirectory(path, archivePath);

        Directory.Delete(path, true);

        string uri = $"{Request.Scheme}://{Request.Host}/images/download/{newGuid}/cropped.zip";
        return Created(uri, uri);
    }

    [HttpGet("download/{id:guid}/cropped.zip")]
    public async Task<ActionResult> Download(Guid id)
    {
        return File(await System.IO.File.ReadAllBytesAsync(
                Path.Combine(environment.ContentRootPath, "wwwroot/archives", id.ToString(), "cropped.zip")),
            "application/zip");
    }

    [HttpGet("get/{path}")]
    public async Task<ActionResult> Get(string path)
    {
        path = path.Replace('_', '/');
        string ext = Path.GetExtension(path).Replace(".", "");
        if (ext == "svg")
            ext = "svg+xml";
        return File(await System.IO.File.ReadAllBytesAsync(
                Path.Combine(environment.ContentRootPath, "wwwroot/images", path)),
            $"image/{ext}");
    }

    [HttpPost("upload")]
    public async Task<ActionResult<string>> Upload([FromForm] List<IFormFile> files)
    {
        IFormFile file = files[0];
        if (!file.ContentType.StartsWith("image/"))
            return BadRequest("File is not an image");

        string newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        using MemoryStream memoryStream = new();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        await mediator.Send(new ScaleDownImageQuery(
            memoryStream,
            Path.Combine(environment.ContentRootPath, "wwwroot/images", newFileName))
        );

        string uri = $"{Request.Scheme}://{Request.Host}/images/get/" + newFileName;
        return Created(uri, uri);
    }
}