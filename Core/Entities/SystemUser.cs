using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class SystemUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual string? ModifiedBy { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual List<Card> Cards { get; set; }
    }
}
