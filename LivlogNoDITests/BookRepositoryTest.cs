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

        // Explicar no TCC que esses unit tests n�o rodam porque dependem de um DbContext v�lido que n�o est� configurado no projeto de testes.

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