using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Queries.Dto;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Queries
{
    public class GetFavoriteSetsQuery : IRequest<Result<IReadOnlyList<GetFlashCardsSetDto>>>
    {
        public int MaximumNumberOfWords { get; set; }
    }

    internal class GetCardsSetsQueryHandler : IRequestHandler<GetFavoriteSetsQuery, Result<IReadOnlyList<GetFlashCardsSetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetCardsSetsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }


        public async Task<Result<IReadOnlyList<GetFlashCardsSetDto>>> Handle(GetFavoriteSetsQuery request, CancellationToken cancellationToken)
        {
            var filteredSets = await _unitOfWork.GetRepository<FlashCardSetFavorite>().Entities
                    .Where(favorite => favorite.AppUserId == _userAccessor.GetUserId())
                    .Select(favorite => favorite.Set)
                    .ProjectTo<GetFlashCardsSetDto>(_mapper.ConfigurationProvider, new { maximumNumberOfWords = request.MaximumNumberOfWords })
                    .ToListAsync(cancellationToken);

            return await Result<IReadOnlyList<GetFlashCardsSetDto>>.Success(filteredSets).ToTask();
        }
    }
}