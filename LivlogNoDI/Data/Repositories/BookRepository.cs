using LivlogNoDI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivlogNoDI.Data.Repositories
{
    public class BookRepository
    {
        private readonly LivlogNoDIContext _dbContext;

        public BookRepository()
        {
            _dbContext = new LivlogNoDIContext(
                new DbContextOptions<LivlogNoDIContext>());
        }

        public List<Book> GetAll()
        {
            return _dbContext.Books
                .AsNoTracking()
                .OrderByDescending(b => b.Id)
                .ToList();
        }

        public Book Get(int bookId)
        {
            return _dbContext.Books
                .AsNoTracking()
                .Where(b => b.Id == bookId)
                .SingleOrDefault()
                    ?? throw new ArgumentException();
        }

        public Book Add(Book book)
        {
            _dbContext.Add(book);
            _dbContext.SaveChanges();

            return book;
        }
    }
}
