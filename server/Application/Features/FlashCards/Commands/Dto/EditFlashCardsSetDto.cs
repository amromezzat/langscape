using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Features.FlashCards.Queries.Dto
{
    public class EditFlashCardsSetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FlashCardsWord> CreatedWords { get; set; } = Array.Empty<FlashCardsWord>();
        public IEnumerable<FlashCardsWord> UpdatedWords { get; set; } = Array.Empty<FlashCardsWord>();
        public IEnumerable<Guid> DeletedWords { get; set; } = Array.Empty<Guid>();
    }
}