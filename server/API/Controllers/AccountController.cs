using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Infrastructure.Security.Dto;
using Langscape.Shared;
using Langscape.Shared.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<IResult<UserDto>>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(result)
            {
                return Result<UserDto>.Success(CreateUserObject(user));
            }

            return Result<UserDto>.Failure("Unauthorized").WithCode(401);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }
    }
}