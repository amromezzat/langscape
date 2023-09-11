using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Features.FlashCards.Queries.Dto
{
    public class EditFlashCardsSetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FlashCardsWord> CreatedWords { get; set; } = new FlashCardsWord[0];
        public IEnumerable<FlashCardsWord> UpdatedWords { get; set; } = new FlashCardsWord[0];
        public IEnumerable<Guid> DeletedWords { get; set; } = new Guid[0];
    }
}