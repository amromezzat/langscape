using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Queries.Dto;
using Application.Features.FlashCards.Services;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Commands
{
    public class GetCardsSetsQuery : IRequest<Result<IReadOnlyList<GetFlashCardsSetDto>>>
    {
        public bool OnlyUserCreatedSets { get; set; }
        public bool OnlyUserFavoriteSets { get; set; }
        public string UserId { get; set; }
        public int MaximumNumberOfWords { get; set; }
    }

    internal class GetCardsSetsQueryHandler : IRequestHandler<GetCardsSetsQuery, Result<IReadOnlyList<GetFlashCardsSetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IFavoriteSetsService _cardsSetService;

        public GetCardsSetsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, IFavoriteSetsService cardsSetService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _cardsSetService = cardsSetService;
        }


        public async Task<Result<IReadOnlyList<GetFlashCardsSetDto>>> Handle(GetCardsSetsQuery request, CancellationToken cancellationToken)
        {
            var allSetsQuery = _unitOfWork.GetRepository<FlashCardsSet>()
                .Entities
                .AsNoTracking();

            var userId = request.OnlyUserCreatedSets ? _userAccessor.GetUserId() : request.UserId;
            if(!string.IsNullOrEmpty(userId))
            {
                allSetsQuery = allSetsQuery.Where(set => set.CreatedById == userId);
            }

            var currentUserFavorites = await _cardsSetService.GetCurrentUserFavoriteSets(cancellationToken);
            if(request.OnlyUserFavoriteSets)
            {
                allSetsQuery = allSetsQuery.Where(set => currentUserFavorites.Contains(set.Id));
            }

            var filteredSets = await allSetsQuery
                    .ProjectTo<GetFlashCardsSetDto>(_mapper.ConfigurationProvider, 
                        new { 
                            maximumNumberOfWords = request.MaximumNumberOfWords,
                            favorites = currentUserFavorites
                        })
                    .ToListAsync(cancellationToken);

            return await Result<IReadOnlyList<GetFlashCardsSetDto>>.Success(filteredSets).ToTask();
        }
    }
}