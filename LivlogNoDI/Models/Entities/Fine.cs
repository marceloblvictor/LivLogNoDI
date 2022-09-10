using LivlogNoDI.Enums;

namespace LivlogNoDI.Models.Entities
{
    public class Fine
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public FineStatus Status { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}