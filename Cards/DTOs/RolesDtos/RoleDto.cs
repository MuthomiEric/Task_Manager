
using System.ComponentModel.DataAnnotations;

namespace Cards.DTOs.RolesDtos
{
    public class RoleDto
    {
        [Required] public string Name { get; set; }
    }
}
