using System;

namespace Application.Features.FlashCards.Queries.Dto
{
    public class GetFlashCardsWordDto
    {
        public Guid Id { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
    }
}