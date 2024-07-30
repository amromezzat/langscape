using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Dto;
using Application.Features.Users.Services;
using Application.Services;
using Application.Users.Dto;
using Langscape.Shared.Impl;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class RegisterCommand : IRequest<Result<AuthUserDto>>
    {
        public RegisterDto RegisterDto { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthUserDto>>
    {
        private readonly IUserManager _userAuthenticator;
        private readonly IUserService _userService;

        public RegisterCommandHandler(IUserManager userAuthenticator, IUserService userService)
        {
            _userAuthenticator = userAuthenticator;
            _userService = userService;
        }

        public async Task<Result<AuthUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userAuthenticator.Register(request.RegisterDto);
            return Result<AuthUserDto>.Success(_userService.CreateAuthUserDto(user));
        }
    }
}