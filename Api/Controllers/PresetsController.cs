using Croptor.Api.ViewModels.Preset;
using Croptor.Api.ViewModels.Size;
using Croptor.Application.Presets.Commands.AddCustomSize;
using Croptor.Application.Presets.Commands.CreatePreset;
using Croptor.Application.Presets.Commands.DeletePreset;
using Croptor.Application.Presets.Commands.UpdatePreset;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SavePreset(PresetDto presetDto)
        {
            IRequest<Preset> command;

            if (!presetDto.Id.HasValue)
            {
                command = new CreatePresetCommand(presetDto.Name, presetDto.Sizes);
            }
            else
            {
                command = new UpdatePresetCommand(presetDto.Id.Value,
                    presetDto.Name,
                    presetDto.Sizes);
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DelatePreset(Guid id)
        {
            await _mediator.Send(new DelatePresetCommand(id));

            return NoContent();
        }
    }
}