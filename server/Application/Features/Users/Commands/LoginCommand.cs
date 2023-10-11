using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Services;
using Application.Services;
using Application.Users.Dto;
using Langscape.Shared.Implementation;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class LoginCommand : IRequest<Result<AuthUserDto>>
    {
        public LoginDto LoginDto { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthUserDto>>
    {
        private readonly IUserManager _userAuthenticator;
        private readonly IUserService _userService;

        public LoginCommandHandler(IUserManager userAuthenticator, IUserService userService)
        {
            _userAuthenticator = userAuthenticator;
            _userService = userService;
        }

        public async Task<Result<AuthUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userAuthenticator.SignIn(request.LoginDto.Email, request.LoginDto.Password);

            if(user != null)
            {
                return Result<AuthUserDto>.Success(_userService.CreateAuthUserDto(user));
            }

            return Result<AuthUserDto>.Failure("Unauthorized").WithCode(401);
        }
    }
}