using ImageMagick;
using MediatR;

namespace Croptor.Application.Images.Queries.ScaleDownImage;

public class ScaleDownImageQueryHandler : IRequestHandler<ScaleDownImageQuery>
{
    public async Task Handle(ScaleDownImageQuery request, CancellationToken cancellationToken)
    {
        using MagickImage image = new MagickImage(request.MemoryStream);

        if (image is { BaseWidth: > 400, BaseHeight: > 400 })
        {
            image.Resize(new MagickGeometry(400, 400)
            {
                IgnoreAspectRatio = false,
                FillArea = true
            });
        }
        if (image.Width > image.Height)
        {
            image.Crop(image.Height, image.Height, Gravity.Center);
        }
        else
        {
            image.Crop(image.Width, image.Width, Gravity.Center);
        }
        await image.WriteAsync(request.SavePath,cancellationToken);
    }
}