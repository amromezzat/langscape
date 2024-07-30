using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Langscape.Shared.Impl;
using MediatR;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Commands
{
    public class CreateCardsSetCommand : IRequest<Result<string>>
    {
        public FlashCardsSet FlashCardSet { get; set; }
    }

    internal class CreateCardsSetCommandHandler : IRequestHandler<CreateCardsSetCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCardsSetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateCardsSetCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.GetRepository<FlashCardsSet>()
                .AddAsync(command.FlashCardSet);

            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success(command.FlashCardSet.Id.ToString(), "Set have been created.");
        }
    }
}