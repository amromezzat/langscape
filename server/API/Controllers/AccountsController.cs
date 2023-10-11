using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Users.Dto;
using Langscape.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<IResult<AuthUserDto>>> Login(CancellationToken cancellationToken, LoginDto loginDto)
        {
            return await _mediator.Send(new LoginCommand { LoginDto = loginDto }, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IResult<UserDto>>> GetUser(CancellationToken cancellationToken, string id)
        {
            return await _mediator.Send(new GetUserQuery { UserId = id }, cancellationToken);
        }
    }
}