using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Queries
{
    public class GetFlashCardQuery : IRequest<Result<FlashCardSet>>
    {
        public Guid SetId { get; set; }
    }

    internal class GetFlashCardQueryHandler : IRequestHandler<GetFlashCardQuery, Result<FlashCardSet>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFlashCardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FlashCardSet>> Handle(GetFlashCardQuery request, CancellationToken cancellationToken)
        {
            var flashCardSet = await _unitOfWork.GetRepository<FlashCardSet>()
                    .Entities
                    .Include(e => e.Words)
                    .FirstOrDefaultAsync(e => e.Id == request.SetId);

            return await Result<FlashCardSet>.Success(flashCardSet).ToTask();
        }
    }
}