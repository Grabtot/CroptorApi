using Croptor.Application.Orders.Queries.CreateWayForPay.Preset;
using Croptor.Application.Orders.Queries.CreateWayForPay.Size;
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
    [Authorize]
    public class PresetsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PresetsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("size")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddCustomSize(SizeDto sizeDto)
        {
            Size size = _mapper.Map<Size>(sizeDto);

            Preset result = await _mediator.Send(new AddCustomSizeCommand(size));

            await _mediator.Publish(new SizeAdded(result, size));

            return NoContent();
        }

        [HttpDelete("size")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveCustomSize(SizeDto sizeDto)
        {
            Size size = _mapper.Map<Size>(sizeDto);

            Preset? result = await _mediator.Send(new RemoveCustomSizeCommand(size));

            if (result != null)
                await _mediator.Publish(new SizeRemoved(result, size));

            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Guid>>> GetPresets()
        {
            List<Guid> result = await _mediator.Send(new GetPresetsQuery());

            return Ok(result);
        }

        [HttpGet("sizes/custom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SizeDto>>> GetCustomSizes()
        {
            Preset preset = await _mediator.Send(new GetCustomSizesQuery());
            List<SizeDto> result = preset.Sizes.ConvertAll(_mapper.Map<SizeDto>);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PresetDto>> GetPreset(Guid id)
        {
            Preset preset = await _mediator.Send(new GetPresetQuery(id));
            PresetDto result = _mapper.Map<PresetDto>(preset);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Policy = "ProPlan")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SavePreset(PresetDto presetDto)
        {
            Preset preset = _mapper.Map<Preset>(presetDto);

            await _mediator.Send(new SavePresetCommand(preset));

            return NoContent();
        }
    }
}