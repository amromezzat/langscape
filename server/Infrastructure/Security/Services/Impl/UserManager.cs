using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
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

        public async Task<AppUser> GetUser(string id) 
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}