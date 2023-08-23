using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Langscape.Shared;
using Langscape.Shared.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Commands
{
    public class GetFlashCardsQuery : IRequest<Result<IReadOnlyList<FlashCardSet>>>
    {
    }

    internal class GetFlashCardsQueryHandler : IRequestHandler<GetFlashCardsQuery, Result<IReadOnlyList<FlashCardSet>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFlashCardsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<FlashCardSet>>> Handle(GetFlashCardsQuery request, CancellationToken cancellationToken)
        {
            var flashCards = await _unitOfWork.GetRepository<FlashCardSet>()
                    .Entities
                    .Include(s => s.Words)
                    .ToListAsync(cancellationToken);

            return await Result<IReadOnlyList<FlashCardSet>>.Success(flashCards).ToTask();
        }
    }
}