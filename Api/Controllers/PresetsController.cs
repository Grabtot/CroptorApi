using Croptor.Api.ViewModels.Preset;
using Croptor.Api.ViewModels.Size;
using Croptor.Application.Presets.Commands.AddCustomSize;
using Croptor.Application.Presets.Commands.SavePreset;
using Croptor.Application.Presets.Queries;
using Croptor.Application.Presets.Queries.GetCustomSizes;
using Croptor.Application.Presets.Queries.GetPreset;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddCustomSize(AddSizeDto sizeDto)
        {
            Size size = _mapper.Map<Size>(sizeDto);

            Preset result = await _mediator.Send(new AddCustomSizeCommand(size));

            await _mediator.Publish(new SizeAdded(result, size));

            return NoContent();
        }

        [Authorize]
        [HttpGet("presets")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<Guid>>> GetPresets()
        {
            List<Guid> result = await _mediator.Send(new GetPresetsQuery());

            return Ok(result);
        }

        [Authorize]
        [HttpGet("sizes/custom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Preset>> GetCustomSizes()
        {
            Preset result = await _mediator.Send(new GetCustomSizesQuery());

            return Ok(result);
        }

        [HttpGet("preset/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<Guid>>> GetPreset(Guid id)
        {
            Preset result = await _mediator.Send(new GetPresetQuery(id));

            return Ok(result);
        }
        
        [HttpPut("preset")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<Guid>>> SavePreset(SavePresetDto presetDto)
        {
            Preset preset = _mapper.Map<Preset>(presetDto);
            
            await _mediator.Send(new SavePresetCommand(preset));

            return NoContent();
        }
    }
}