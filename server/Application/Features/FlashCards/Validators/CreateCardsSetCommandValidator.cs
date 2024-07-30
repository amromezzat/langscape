using Application.Features.FlashCards.Commands;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.FlashCards.Validators
{
    public class CreateCardsSetCommandValidator : AbstractValidator<CreateCardsSetCommand>
    {
        public CreateCardsSetCommandValidator()
        {
            RuleFor(x => x.FlashCardSet).SetValidator(new CreateCardsSetValidator());
        }
    }

    public class CreateCardsSetValidator : AbstractValidator<FlashCardsSet>
    {
        public CreateCardsSetValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Set name can't be empty");
            RuleFor(x => x.Words).NotEmpty().WithMessage("Set must contain at least one word");
        }
    }
}