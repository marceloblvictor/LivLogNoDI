using LivlogNoDI.Models.DTO;

namespace LivlogNoDI.Services.Interfaces
{
    public interface IBookService
    {
        BookDTO Get(int bookId);
        IEnumerable<BookDTO> GetAll();
    }
}