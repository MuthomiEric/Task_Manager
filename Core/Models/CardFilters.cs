using Core.Utils;

namespace Core.Models
{
    public class CardFilters
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public Status? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
