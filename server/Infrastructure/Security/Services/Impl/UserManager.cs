using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Users.Dto;
using Application.Services;
using Domain.Entities;
using Infrastructure.Security.Exceptions.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Exceptions.Impl;

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
                throw new InvalidLoginCredentialsException();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if(!isPasswordValid) 
            {
                throw new InvalidLoginCredentialsException();
            }

            return user;
        }

        public async Task<AppUser> Register(RegisterDto registerDto)
        {
            if(await _userManager.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new UniqueFieldException("Email already exists");
            }

            if(await _userManager.Users.AnyAsync(u => u.UserName == registerDto.Username))
            {
                throw new UniqueFieldException("Username already exists");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
            {
                throw new ValidationException(result.Errors.First().Description);
            }

            return user;
        }

        public async Task<AppUser> GetUserById(string id) 
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                throw new NotFoundException($"Couldn't find user with id {id}");
            }

            return user;
        }

        public async Task<AppUser> GetUserByUsername(string username) 
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if(user == null)
            {
                throw new NotFoundException($"Couldn't find user with username {username}");
            }

            return user;
        }

        public async Task<bool> IsValidUserId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            return await _userManager.Users.AnyAsync(u => u.Id == id);
        }
    }
}