using Core.Utils;
using System.ComponentModel.DataAnnotations;

namespace Cards.DTOs.CardDtos
{
    public class CardToEditDto
    {
        [Required] public string Name { get; set; }

        [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color should be 6 alphanumeric characters prefixed with #")]
        public string? Color { get; set; }
        public string? Description { get; set; }
        [Required] public Status Status { get; set; }
    }
}
