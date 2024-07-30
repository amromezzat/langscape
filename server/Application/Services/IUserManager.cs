using System.Threading.Tasks;
using Application.Features.Users.Dto;
using Domain.Entities;

namespace Application.Services
{
    public interface IUserManager
    {
        /// <returns>User if corrected credentials are passed or null otherwise</returns>
        /// <exception cref="InvalidLoginCredentialsException">Thrown if the email is not registered or a wrong password with provided.</exception>
        Task<AppUser> SignIn(string email, string password);

        /// <returns>New user if there are no duplicate email or username and other fields are valid</returns>
        /// <exception cref="UniqueFieldException">Thrown a unique field like email or username is already registered</exception>
        /// <exception cref="ValidationException">Thrown when account creation fails</exception>
        Task<AppUser> Register(RegisterDto registerDto);

        /// <returns>Returns user that matches the provided id</returns>
        /// <exception cref="NotFoundException">Thrown when the user with the provided id doesn't exist</exception>
        Task<AppUser> GetUserById(string id);

        /// <returns>Returns user that matches the provided username</returns>
        /// <exception cref="NotFoundException">Thrown when the user with the provided username doesn't exist</exception>
        Task<AppUser> GetUserByUsername(string username);

        /// <returns>Returns true if user id is valid, or false otherwise</returns>
        Task<bool> IsValidUserId(string id);
    }
}