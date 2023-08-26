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
    public class GetFlashCardQuery : IRequest<Result<FlashCardSetDto>>
    {
        public Guid SetId { get; set; }
    }

    internal class GetFlashCardQueryHandler : IRequestHandler<GetFlashCardQuery, Result<FlashCardSetDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFlashCardQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<FlashCardSetDto>> Handle(GetFlashCardQuery request, CancellationToken cancellationToken)
        {
            var flashCardSet = await _unitOfWork.GetRepository<FlashCardSet>()
                    .Entities
                    .ProjectTo<FlashCardSetDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(e => e.Id == request.SetId);

            return await Result<FlashCardSetDto>.Success(flashCardSet).ToTask();
        }
    }
}