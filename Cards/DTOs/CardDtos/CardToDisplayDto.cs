using Core.Utils;

namespace Cards.DTOs.CardDtos
{
    public class CardToDisplayDto : BaseDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
    }
}
