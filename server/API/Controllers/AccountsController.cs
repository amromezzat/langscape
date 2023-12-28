using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Commands;
using Application.Features.Users.Dto;
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<IResult<AuthUserDto>>> Register(CancellationToken cancellationToken, RegisterDto registerDto)
        {
            return await _mediator.Send(new RegisterCommand { RegisterDto = registerDto }, cancellationToken);
        }

        [HttpGet("current")]
        public async Task<ActionResult<IResult<UserDto>>> GetCurrentUser(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetUserQuery { Username = User.Identity.Name }, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IResult<UserDto>>> GetUserById(CancellationToken cancellationToken, string id)
        {
            return await _mediator.Send(new GetUserQuery { UserId = id }, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<IResult<UserDto>>> GetUserByUsername(CancellationToken cancellationToken, [FromQuery] string username)
        {
            return await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);
        }
    }
}