namespace LivlogNoDI.Models.DTO
{
    public class RentalRequestDTO
    {        
        public int CustomerId { get; set; }
        public IList<int> BookIds { get; set; }
    }
}
