using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }

        public virtual string? ModifiedBy { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        public virtual DateTime CreatedDate { get; set; }
    }
}
