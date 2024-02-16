using Cards.DTOs.CardDtos;
using Core.Entities;

namespace Cards.Extensions.Entity
{
    public static class CardEntityHelper
    {
        public static void Update(this Card card, CardToEditDto cardDto, string modifiedBy, DateTime modifiedDate)
        {
            card.Name = cardDto.Name;
            card.Color = cardDto.Color;
            card.Status = cardDto.Status;
            card.ModifiedBy = modifiedBy;
            card.ModifiedDate = modifiedDate;
            card.Description = cardDto.Description;
        }

        public static void Patch(this Card card, CardToPatchDto cardDto, string modifiedBy, DateTime modifiedDate)
        {
            card.ModifiedBy = modifiedBy;
            card.ModifiedDate = modifiedDate;
            card.Name = cardDto.Name ?? card.Name;
            card.Color = cardDto.Color ?? card.Color;
            card.Status = cardDto.Status ?? card.Status;
            card.Description = cardDto.Description ?? card.Description;
        }
    }
}
