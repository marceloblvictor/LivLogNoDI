using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;

namespace LivlogNoDI.Services
{
    public class BookService
    {
        private readonly BookRepository _repo;

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

        public BookDTO Create(BookDTO bookDTO)
        {
            var book = CreateEntity(bookDTO);

            book = _repo.Add(book);

            return CreateDTO(book);
        }

        public BookDTO Update(int bookId, BookDTO bookDTO)
        {
            var book = _repo.Get(bookId);

            book.Title = bookDTO.Title;
            book.ISBN = bookDTO.ISBN;
            book.Quantity = bookDTO.Quantity;

            var updatedBook = 
                _repo.Update(book);

            return CreateDTO(updatedBook);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);            
        }

        private BookDTO CreateDTO(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Quantity = book.Quantity
            };
        }

        private IEnumerable<BookDTO> CreateDTOs(IEnumerable<Book> books)
        {
            var booksDtos = new List<BookDTO>();

            foreach (var book in books)
            {
                booksDtos.Add(CreateDTO(book));
            }

            return booksDtos;
        }

        private Book CreateEntity(BookDTO dto)
        {
            return new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                ISBN = dto.ISBN,
                Quantity = dto.Quantity
            };
        }
    }
}
