using System;
using System.Collections.Generic;

namespace Application.Features.FlashCards.Queries.Dto
{
    public class FlashCardSetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<FlashCardWordDto> Words { get; set; } = new List<FlashCardWordDto>();
    }
}