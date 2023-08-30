using Domain.Entities;

namespace Application.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}