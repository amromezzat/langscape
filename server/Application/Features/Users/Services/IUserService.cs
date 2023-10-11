using Application.Users.Dto;
using Domain.Entities;

namespace Application.Features.Users.Services
{
    public interface IUserService
    {
        AuthUserDto CreateAuthUserDto(AppUser user);
        UserDto CreateUserDto(AppUser user);
    }
}