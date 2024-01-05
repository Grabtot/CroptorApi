﻿using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using MediatR;

namespace Croptor.Application.Presets.Commands.AddCustomSize
{
    public record AddCustomSizeCommand(Size Size) : IRequest<Preset>
    {
    }
}
