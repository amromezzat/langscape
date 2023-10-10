using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Queries.Dto;
using Application.Features.FlashCards.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Queries
{
    public class GetCardsSetQuery : IRequest<Result<GetFlashCardsSetDto>>
    {
        public Guid SetId { get; set; }
    }

    internal class GetCardsSetQueryHandler : IRequestHandler<GetCardsSetQuery, Result<GetFlashCardsSetDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFavoriteSetsService _cardsSetService;

        public GetCardsSetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IFavoriteSetsService cardsSetService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cardsSetService = cardsSetService;
        }

        public async Task<Result<GetFlashCardsSetDto>> Handle(GetCardsSetQuery request, CancellationToken cancellationToken)
        {
            var currentUserFavorites = await _cardsSetService.GetCurrentUserFavoriteSets(cancellationToken);
            var flashCardSet = await _unitOfWork.GetRepository<FlashCardsSet>()
                .Entities
                .AsNoTracking()
                .ProjectTo<GetFlashCardsSetDto>(_mapper.ConfigurationProvider, new { favorites = currentUserFavorites })
                .FirstOrDefaultAsync(e => e.Id == request.SetId);

            return await Result<GetFlashCardsSetDto>.Success(flashCardSet).ToTask();
        }
    }
}