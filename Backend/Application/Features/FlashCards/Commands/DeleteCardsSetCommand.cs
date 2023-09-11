using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Persistence.Repositories;
using Persistence.Repositories.Implementation;

namespace Application.Features.FlashCards.Commands.Dto
{
    public class DeleteCardsSetCommand : IRequest<Result<UnitOfWork>>
    {
        public Guid Id { get; set; }
    }

    internal class EditCardsSetCommandHandler : IRequestHandler<DeleteCardsSetCommand, Result<UnitOfWork>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditCardsSetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UnitOfWork>> Handle(DeleteCardsSetCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.GetRepository<FlashCardsSet>().DeleteAsync(command.Id);
            await _unitOfWork.Save(cancellationToken);

            return Result<UnitOfWork>.Success("Set have been deleted.");
        }
    }
}