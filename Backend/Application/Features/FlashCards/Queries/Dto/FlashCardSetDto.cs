using System.Collections.Generic;
using Domain.Entities;

namespace Application.Features.FlashCards.Queries.Dto
{
    public class FlashCardSetDto
    {
        public string Name { get; set; }
        public ICollection<FlashCardWord> Words { get; set; } = new List<FlashCardWord>();
    }
}