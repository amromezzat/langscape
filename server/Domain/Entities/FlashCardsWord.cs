using Domain.Common.Implementation;

namespace Domain.Entities
{
    public class FlashCardsWord : EntityBase
    {
        public string Word { get; set; }
        public string Translation { get; set; }
        public int Position { get; set; }
        public FlashCardsSet Set { get; set; }
    }
}