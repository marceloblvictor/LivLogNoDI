using LivlogNoDI.Models.Entities;

namespace LivlogNoDI.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Book Add(Book book);
        Book Get(int bookId);
        List<Book> GetAll();
    }
}