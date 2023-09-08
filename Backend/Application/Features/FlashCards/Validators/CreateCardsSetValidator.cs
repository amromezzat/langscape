using Domain.Entities;
using FluentValidation;

namespace Application.Features.FlashCards.Validators
{
    public class CreateCardsSetValidator : AbstractValidator<FlashCardSet>
    {
        public CreateCardsSetValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Set name can't be empty");
            RuleFor(x => x.Words).NotEmpty().WithMessage("Set must contain at least one word");
        }
    }
}