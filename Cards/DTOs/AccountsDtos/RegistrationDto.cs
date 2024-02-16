using System.ComponentModel.DataAnnotations;

namespace Cards.DTOs.AccountsDtos
{
    public class RegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
 
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password should be atleast six characters in length")]
        public string Password { get; set; }
    }
}
