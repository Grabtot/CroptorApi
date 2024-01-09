using Croptor.Api.ViewModels.Image;
using FluentValidation;

namespace Croptor.Application.Images.Queries.CropImage;

public class CropImageQueryValidator : AbstractValidator<CropImageQuery>
{
    public CropImageQueryValidator()
    {
        RuleFor(query => query.MemoryStream).NotNull().WithMessage("MemoryStream cannot be null.");
        RuleFor(query => query.FileName).NotEmpty().WithMessage("FileName cannot be empty.");
        RuleFor(query => query.ImageParams).SetValidator(new ImageParamsDtoValidator());
        RuleFor(query => query.Sizes).NotEmpty().WithMessage("Sizes list cannot be empty.");
        RuleForEach(query => query.Sizes).SetValidator(new CropSizeDtoValidator());
    }
}

public class ImageParamsDtoValidator : AbstractValidator<ImageParamsDto>
{
    public ImageParamsDtoValidator()
    {
        RuleFor(paramsDto => paramsDto.VerticalSnap)
            .Must(snap => snap is "Top" or "Center" or "Bottom")
            .WithMessage("VerticalSnap must be 'Top', 'Center', or 'Bottom'.");

        RuleFor(paramsDto => paramsDto.HorizontalSnap)
            .Must(snap => snap is "Left" or "Center" or "Right")
            .WithMessage("HorizontalSnap must be 'Left', 'Center', or 'Right'.");

        When(paramsDto => paramsDto.Center != null, () =>
        {
            RuleFor(paramsDto => paramsDto.Center!).SetValidator(new CenterDtoValidator());
        });
    }
}

public class CropSizeDtoValidator : AbstractValidator<CropSizeDto>
{
    public CropSizeDtoValidator()
    {
        RuleFor(sizeDto => sizeDto.Width).GreaterThan(0).WithMessage("Width must be greater than 0.");
        RuleFor(sizeDto => sizeDto.Height).GreaterThan(0).WithMessage("Height must be greater than 0.");
    }
}

public class CenterDtoValidator : AbstractValidator<CenterDto>
{
    public CenterDtoValidator()
    {
        RuleFor(centerDto => centerDto.X)
            .NotEmpty().WithMessage("X coordinate cannot be empty.")
            .GreaterThan(0).WithMessage("X coordinate must be greater than 0.");
        RuleFor(centerDto => centerDto.Y)
            .NotEmpty().WithMessage("Y coordinate cannot be empty.")
            .GreaterThan(0).WithMessage("Y coordinate must be greater than 0.");
    }
}