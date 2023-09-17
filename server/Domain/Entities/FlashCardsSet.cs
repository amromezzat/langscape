using System.Collections.Generic;
using Domain.Common.Implementation;

namespace Domain.Entities
{
    public class FlashCardsSet : AuditableEntityBase
    {
        public string Name { get; set; }
        public ICollection<FlashCardsWord> Words { get; set; } = new List<FlashCardsWord>();
    }
}