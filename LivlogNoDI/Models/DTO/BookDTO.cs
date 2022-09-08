using System.Text.Json.Serialization;

namespace LivlogNoDI.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
    }
}
