using Application.Services;
using Application.Users.Dto;
using Domain.Entities;

namespace Application.Features.Users.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;

        public UserService(ITokenService tokenService) 
        {
            _tokenService = tokenService;
        }

        public AuthUserDto CreateAuthUserDto(AppUser user)
        {
            return new AuthUserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }

        public UserDto CreateUserDto(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Username = user.UserName
            };
        }
    }
}