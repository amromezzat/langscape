using Application.Features.FlashCards.Queries.Dto;
using FluentValidation;
using Shared.Extensions;

namespace Application.Features.FlashCards.Validators
{
    public class EditCardsSetValidator : AbstractValidator<EditFlashCardsSetDto>
    {
        public EditCardsSetValidator()
        {
            RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.Name)
                || !x.CreatedWords.IsNullOrEmpty()
                || !x.UpdatedWords.IsNullOrEmpty()
                || !x.DeletedWords.IsNullOrEmpty())
            .WithMessage("There are no provided changes.");
        }
    }
}