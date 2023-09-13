using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Commands.Dto
{
    public class DeleteCardsSetCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    internal class EditCardsSetCommandHandler : IRequestHandler<DeleteCardsSetCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditCardsSetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteCardsSetCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.GetRepository<FlashCardsSet>().DeleteAsync(command.Id);
            await _unitOfWork.Save(cancellationToken);

            return Result<Unit>.Success("Set have been deleted.");
        }
    }
}