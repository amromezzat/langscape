using System;
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

        public GetCardsSetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetFlashCardsSetDto>> Handle(GetCardsSetQuery request, CancellationToken cancellationToken)
        {
            var flashCardSet = await _unitOfWork.GetRepository<FlashCardsSet>()
                    .Entities
                    .ProjectTo<GetFlashCardsSetDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(e => e.Id == request.SetId);

            return await Result<GetFlashCardsSetDto>.Success(flashCardSet).ToTask();
        }
    }
}