using Croptor.Api.ViewModels.User;
using Croptor.Application.Users.Commands.SaveUser;
using Croptor.Application.Users.Queries.GetUser;
using Croptor.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Croptor.Api.Controllers
{
    [Authorize]
    [Route("/api/user")]
    [ApiController]
    public class UserControler(
        IMapper mapper,
        IMediator mediator
        ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            User user = await mediator.Send(new GetUserQuery());

            return Ok(mapper.Map<UserDto>(user));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> SaveUser(SaveUserDto dto)
        {
            await mediator.Send(new SaveUserCommand(dto));
            return NoContent();
        }
    }
}

