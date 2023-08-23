using System.Collections.Generic;
using Domain.Common.Implementation;

namespace Domain.Entities
{
    public class FlashCardSet : AuditableEntityBase
    {
        public ICollection<FlashCardWord> Words { get; set; } = new List<FlashCardWord>();
    }
}