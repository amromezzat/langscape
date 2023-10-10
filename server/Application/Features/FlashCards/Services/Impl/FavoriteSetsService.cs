using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Services.Impl
{
    public class FavoriteSetsService : IFavoriteSetsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;

        public FavoriteSetsService(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
        }

        public async Task<List<Guid>> GetCurrentUserFavoriteSets(CancellationToken cancellationToken) 
        {
            return await GetUserFavoriteSets(cancellationToken, _userAccessor.GetUserId());
        }

        public async Task<List<Guid>> GetUserFavoriteSets(CancellationToken cancellationToken, string userId)
        {
            return await _unitOfWork.GetRepository<FlashCardSetFavorite>()
                .Entities
                .AsNoTracking()
                .Where(favorite => favorite.AppUserId == userId)
                .Select(favorite => favorite.SetId)
                .ToListAsync(cancellationToken);
        } 
    }
}