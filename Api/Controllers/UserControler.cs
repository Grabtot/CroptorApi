using Croptor.Api.ViewModels.User;
using Croptor.Application.Users.Queries;
using Croptor.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Croptor.Api.Controllers
{
    [Authorize]
    [Route("user")]
    [ApiController]
    public class UserControler(
        IMapper mapper,
        IMediator mediator
        ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            User user = await mediator.Send(new GetUserQuery());
            return Ok(mapper.Map<UserDto>(user));
        }
    }
}

