using System;
using Domain.Common.Implementation;

namespace Domain.Entities
{
    public class FlashCardSetFavorite : EntityBase
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid SetId { get; set; }
        public FlashCardsSet Set { get; set; }
    }
}