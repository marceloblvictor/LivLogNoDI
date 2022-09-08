namespace LivlogNoDI.Models.DTO
{
    public class CustomerBookDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
