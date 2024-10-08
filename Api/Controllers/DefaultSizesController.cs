﻿using Croptor.Application.DefaultSizes.Commands.AddCategory;
using Croptor.Application.DefaultSizes.Commands.AddSize;
using Croptor.Application.DefaultSizes.Commands.EditCategory;
using Croptor.Application.DefaultSizes.Commands.EditSize;
using Croptor.Application.DefaultSizes.Commands.RemoveCategory;
using Croptor.Application.DefaultSizes.Commands.RemoveSize;
using Croptor.Application.DefaultSizes.Queries.GetCategories;
using Croptor.Domain.Presets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Croptor.Api.Controllers;

[Authorize(Policy = "Admin")]
[ApiController]
[Route("api/default")]
public class DefaultSizesController(IMediator mediator) : ControllerBase
{
    [HttpGet("categories")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Preset>>> GetCategories()
    {
        List<Preset> result = await mediator.Send(new GetCategoriesQuery());

        return Ok(result);
    }

    [HttpPost("size")]
    public async Task<ActionResult> AddSize(AddSizeCommand dto)
    {
        await mediator.Send(dto);

        return NoContent();
    }

    [HttpPost("category")]
    public async Task<ActionResult> AddCategory(AddCategoryCommand dto)
    {
        Guid id = await mediator.Send(dto);

        return Created(id.ToString(), id.ToString());
    }

    [HttpDelete("size")]
    public async Task<ActionResult> RemoveSize(RemoveSizeCommand dto)
    {
        await mediator.Send(dto);

        return NoContent();
    }

    [HttpDelete("category")]
    public async Task<ActionResult> RemoveCategory(RemoveCategoryCommand dto)
    {
        await mediator.Send(dto);

        return NoContent();
    }

    [HttpPut("size")]
    public async Task<ActionResult> EditSize(EditSizeCommand dto)
    {
        await mediator.Send(dto);

        return NoContent();
    }

    [HttpPut("category")]
    public async Task<ActionResult> EditCategory(EditCategoryCommand dto)
    {
        await mediator.Send(dto);

        return NoContent();
    }
}