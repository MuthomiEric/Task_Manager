namespace Cards.DTOs
{
    public class BaseDto
    {
        public Guid Id { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
