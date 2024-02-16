using Core.Utils;

namespace Core.Entities
{
    public class Card : BaseEntity
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public string? Description { get; set; }
        public string OwnerId { get; set; }
        public virtual SystemUser Owner { get; set; }
    }
}
