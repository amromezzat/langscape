using System;
using System.Collections.Generic;

namespace Application.Features.FlashCards.Queries.Dto
{
    public class GetFlashCardsSetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetFlashCardsWordDto> Words { get; set; } = new List<GetFlashCardsWordDto>();
    }
}