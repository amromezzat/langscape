using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Queries.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Commands
{
    public class GetFlashCardsQuery : IRequest<Result<IReadOnlyList<GetFlashCardsSetDto>>>
    {
        public int MaximumNumberOfWords { get; set; }
    }

    internal class GetFlashCardsQueryHandler : IRequestHandler<GetFlashCardsQuery, Result<IReadOnlyList<GetFlashCardsSetDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFlashCardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<IReadOnlyList<GetFlashCardsSetDto>>> Handle(GetFlashCardsQuery request, CancellationToken cancellationToken)
        {
            var flashCards = await _unitOfWork.GetRepository<FlashCardSet>()
                    .Entities
                    .ProjectTo<GetFlashCardsSetDto>(_mapper.ConfigurationProvider, new { maximumNumberOfWords = request.MaximumNumberOfWords })
                    .ToListAsync(cancellationToken);

            return await Result<IReadOnlyList<GetFlashCardsSetDto>>.Success(flashCards).ToTask();
        }
    }
}