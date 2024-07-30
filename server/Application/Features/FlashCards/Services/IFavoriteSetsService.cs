using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.FlashCards.Services
{
    public interface IFavoriteSetsService
    {
        Task<List<Guid>> GetCurrentUserFavoriteSets(CancellationToken cancellationToken);
        Task<List<Guid>> GetUserFavoriteSets(CancellationToken cancellationToken, string userId);
    }
}