using Croptor.Api.ViewModels.Size;
using Croptor.Application.Presets.Commands.AddCustomSize;
using Croptor.Domain.Common.ValueObjects;
using Croptor.Domain.Presets;
using Croptor.Domain.Presets.Events;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Croptor.Api.Controllers
{
    [Route("presets")]
    [ApiController]
    public class PresetsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PresetsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("size")]
        public async Task<ActionResult> AddCustomSize(AddSizeDto sizeDto)
        {
            Size size = _mapper.Map<Size>(sizeDto);

            Preset result = await _mediator.Send(new AddCustomSizeCommand(size));

            await _mediator.Publish(new SizeAdded(result, size));

            return NoContent();
        }
    }
}
