using MediatR;

namespace Croptor.Application.Images.Queries.ScaleDownImage;

public record ScaleDownImageQuery(MemoryStream MemoryStream, string SavePath) : IRequest;