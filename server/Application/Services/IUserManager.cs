using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services
{
    public interface IUserManager
    {
        /// <returns>User if corrected credentials are passed or null otherwise</returns>
        Task<AppUser> SignIn(string email, string password);

        /// <returns>Returns user that matches the provided id</returns>
        Task<AppUser> GetUser(string id);
    }
}