using Domain.Common.Implementation;

namespace Domain.Entities
{
    public class FlashCardWord : EntityBase
    {
        public string Word { get; set; }
        public string Translation { get; set; }
        public FlashCardSet Set { get; set; }
    }
}