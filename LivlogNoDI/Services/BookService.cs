using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Data.Repositories.Interfaces;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;
using LivlogNoDI.Services.Interfaces;

namespace LivlogNoDI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;

        public BookService()
        {
            _repo = new BookRepository();
        }

        public BookDTO Get(int bookId)
        {
            var book = _repo.Get(bookId);

            return CreateDTO(book);
        }

        public IEnumerable<BookDTO> GetAll()
        {
            var books = _repo.GetAll();

            return CreateDTOs(books);
        }

        private BookDTO CreateDTO(Book book)
        {
            return new BookDTO();
        }

        private IEnumerable<BookDTO> CreateDTOs(IEnumerable<Book> books)
        {
            var booksDtos = new List<BookDTO>();

            //foreach (var book in books)
            //{
            //    booksDtos.Add(new BookDTO
            //    {
            //        Id = book.Id,
            //        Title = book.Title,
            //        ISSBN = book.ISSBN,
            //        PagesQuantity = book.PagesQuantity
            //    });
            //}

            return booksDtos;
        }
    }
}
