using System.Linq;
using System.Threading.Tasks;
using Application.Features.Users.Dto;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Security.Services.Impl
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<AppUser> _userManager;

        public UserManager(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> SignIn(string email, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) 
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);

            if(result) 
            {
                return user;
            }

            return null;
        }

        public async Task<AppUser> Register(RegisterDto registerDto)
        {
            if(await _userManager.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new DuplicateEmailException();
            }

            if(await _userManager.Users.AnyAsync(u => u.UserName == registerDto.Username))
            {
                throw new DuplicateUsernameException();
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(result.Succeeded)
            {
                return user;
            }

            throw new RegisterException(result.Errors.First().Description);
        }

        public async Task<AppUser> GetUserById(string id) 
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> GetUserByUsername(string username) 
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}