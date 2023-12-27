using System.Threading.Tasks;
using Application.Features.Users.Dto;
using Domain.Entities;

namespace Application.Services
{
    public interface IUserManager
    {
        /// <returns>User if corrected credentials are passed or null otherwise</returns>
        Task<AppUser> SignIn(string email, string password);

        /// <returns>New user if there are no duplicate email or username and other fields are valid</returns>
        /// <exception cref="RegisterException">Thrown when there is a problem with user registration.</exception>
        Task<AppUser> Register(RegisterDto registerDto);

        /// <returns>Returns user that matches the provided id</returns>
        Task<AppUser> GetUserById(string id);

        /// <returns>Returns user that matches the provided username</returns>
        Task<AppUser> GetUserByUsername(string username);
    }
}