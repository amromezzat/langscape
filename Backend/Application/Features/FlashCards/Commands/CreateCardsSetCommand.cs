using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Validators;
using Domain.Entities;
using FluentValidation;
using Langscape.Shared.Implementation;
using MediatR;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Commands
{
    public class CreateCardsSetCommand : IRequest<Result<string>>
    {
        public FlashCardsSet FlashCardSet { get; set; }
    }

    public class CreateCardsSetCommandValidator : AbstractValidator<CreateCardsSetCommand>
    {
        public CreateCardsSetCommandValidator()
        {
            RuleFor(x => x.FlashCardSet).SetValidator(new CreateCardsSetValidator());
        }
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