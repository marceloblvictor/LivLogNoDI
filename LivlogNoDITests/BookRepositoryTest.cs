using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Models.Entities;

namespace LivlogNoDITests
{
    public class BookRepositoryTest
    {
        BookRepository _repo { get; set; }

        public BookRepositoryTest()
        {
            _repo = new BookRepository();
        }

        // Explicar no TCC que esses unit tests não rodam porque dependem de um DbContext válido que não está configurado no projeto de testes.

        [Fact]
        public void GetAll_ReturnsListOfBooks()
        {
            // Act
            var books = _repo.GetAll();

            // Assert
            Assert.IsType<List<Book>>(books);
        }        
    }
}