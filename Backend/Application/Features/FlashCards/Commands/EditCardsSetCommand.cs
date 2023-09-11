using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Queries.Dto;
using Application.Features.FlashCards.Validators;
using Domain.Entities;
using FluentValidation;
using Langscape.Shared.Implementation;
using MediatR;
using Persistence.Repositories;
using Persistence.Repositories.Implementation;

namespace Application.Features.FlashCards.Commands
{
    public class EditCardsSetCommand : IRequest<Result<UnitOfWork>>
    {
        public EditFlashCardsSetDto FlashCardSet { get; set; }
    }

    public class EditCardsSetCommandValidator : AbstractValidator<EditCardsSetCommand>
    {
        public EditCardsSetCommandValidator()
        {
            RuleFor(x => x.FlashCardSet).SetValidator(new EditCardsSetValidator());
        }
    }

    internal class EditCardsSetCommandHandler : IRequestHandler<EditCardsSetCommand, Result<UnitOfWork>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditCardsSetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UnitOfWork>> Handle(EditCardsSetCommand command, CancellationToken cancellationToken)
        {
            var set =  await _unitOfWork.GetRepository<FlashCardsSet>().GetByIdAsync(command.FlashCardSet.Id);
            
            if (!string.IsNullOrEmpty(command.FlashCardSet.Name))
            {
                set.Name =  command.FlashCardSet.Name;
            }
            
            foreach(var word in command.FlashCardSet.CreatedWords)
            {
                word.Set = set;
            }

            var wordsRepo = _unitOfWork.GetRepository<FlashCardsWord>();
            await wordsRepo.AddRangeAsync(command.FlashCardSet.CreatedWords);
            await wordsRepo.UpdateRangeAsync(command.FlashCardSet.UpdatedWords);
            await wordsRepo.DeleteRangeAsync(command.FlashCardSet.DeletedWords);

            await _unitOfWork.Save(cancellationToken);

            return Result<UnitOfWork>.Success("Set have been updated.");
        }
    }
}