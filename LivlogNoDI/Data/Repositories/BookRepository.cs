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
                .Include(b => b.CustomerBooks)
                .OrderByDescending(b => b.Id)
                .ToList();
        }

        public Book Get(int bookId)
        {
            return _dbContext.Books
                .Include(b => b.CustomerBooks)
                .Where(b => b.Id == bookId)
                .SingleOrDefault()
                    ?? throw new ArgumentException();
        }

        public Book Add(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            book = Get(book.Id);

            return book;
        }

        public Book Update(Book book)
        {
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();

            book = Get(book.Id);

            return book;
        }

        public bool Delete(int bookId)
        {
            var book = Get(bookId);

            if (book is null)
            {
                throw new ArgumentException();
            }
            
            _dbContext.Remove(book);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
