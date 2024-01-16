using ImageMagick;
using MediatR;

namespace Croptor.Application.Images.Queries.CropImage;

public class CropImageQueryHandler : IRequestHandler<CropImageQuery>
{
    public async Task Handle(CropImageQuery request, CancellationToken cancellationToken)
    {
        var dirPath = Path.Combine(request.directoryPath, request.FileName);
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
        foreach (var size in request.Sizes)
        {
            using MagickImage image = new MagickImage(request.MemoryStream);

            if (request.ImageParams.FitNCrop)
            {
                image.Resize(new MagickGeometry(size.Width, size.Height)
                {
                    IgnoreAspectRatio = false,
                    FillArea = true
                });
            }
            else if (size.Width > image.BaseWidth || size.Height > image.BaseHeight)
                continue;

            if (request.ImageParams.Center != null)
            {
                int x = 
                    Clap(request.ImageParams.Center.X * image.Width / image.BaseWidth, 
                        size.Width / 2, 
                        image.Width - size.Width / 2);
                int y =
                    Clap(request.ImageParams.Center.Y * image.Height / image.BaseHeight,
                        size.Height / 2,
                        image.Height - size.Height / 2);
                image.Crop(new MagickGeometry(size.Width, size.Height)
                {
                    X = x - size.Width / 2,
                    Y = y - size.Height / 2
                });
            }
            else
            {
                image.Crop(
                    size.Width,
                    size.Height,
                    SnapToGravity(
                        request.ImageParams.VerticalSnap,
                        request.ImageParams.HorizontalSnap
                    )
                );
            }
            
            var name = Path.GetFileNameWithoutExtension(request.FileName);
            var ext = Path.GetExtension(request.FileName);
            await image.WriteAsync(
                Path.Combine(dirPath, $"{name} {size.Name} {size.Width}x{size.Height}{ext}"),
                cancellationToken);
        }
    }

    private Gravity SnapToGravity(string VerticalSnap, string HorizontalSnap)
    {
        switch ((VerticalSnap, HorizontalSnap))
        {
            case ("Top", "Left"):
                return Gravity.Northwest;
            case ("Top", "Center"):
                return Gravity.North;
            case ("Top", "Right"):
                return Gravity.Northeast;
            case ("Center", "Left"):
                return Gravity.West;
            case ("Center", "Center"):
                return Gravity.Center;
            case ("Center", "Right"):
                return Gravity.East;
            case ("Bottom", "Left"):
                return Gravity.Southwest;
            case ("Bottom", "Center"):
                return Gravity.South;
            case ("Bottom", "Right"):
                return Gravity.Southeast;
            default:
                throw new ArgumentException("Vertical and/or horizontal snap are not one of the allowed");
        }
    }

    private int Clap(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}