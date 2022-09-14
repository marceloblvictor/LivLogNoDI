using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;
using LivlogNoDI.Services;

namespace LivlogNoDITests.ServicesTests
{
    public class BookServiceTest
    {
        BookService _service { get; set; }

        Book ValidBook { get; set; } = new()
        {
            Id = 1,
            Title = "LivroTeste1",
            ISBN = "teste1",
            Quantity = 5
        };

        IList<Book> ValidBooks = new List<Book>()
        {
            new()
            {
                Id = 1,
                Title = "LivroTeste1",
                ISBN = "teste1",
                Quantity = 5
            },
            new()
            {
                Id = 2,
                Title = "LivroTeste2",
                ISBN = "teste2",
                Quantity = 5
            },
            new()
            {
                Id = 3,
                Title = "LivroTeste3",
                ISBN = "teste3",
                Quantity = 5
            },
            new()
            {
                Id = 4,
                Title = "LivroTeste4",
                ISBN = "teste4",
                Quantity = 5
            },
        };

        BookDTO ValidBookDTO { get; set; } = new()
        {
            Id = 1,
            Title = "LivroTeste1",
            ISBN = "teste1",
            Quantity = 5
        };

        IList<BookDTO> ValidBookDTOs = new List<BookDTO>()
        {
            new()
            {
                Id = 1,
                Title = "LivroTeste1",
                ISBN = "teste1",
                Quantity = 5
            },
            new()
            {
                Id = 2,
                Title = "LivroTeste2",
                ISBN = "teste2",
                Quantity = 5
            },
            new()
            {
                Id = 3,
                Title = "LivroTeste3",
                ISBN = "teste3",
                Quantity = 5
            },
            new()
            {
                Id = 4,
                Title = "LivroTeste4",
                ISBN = "teste4",
                Quantity = 5
            },
        };

        public BookServiceTest()
        {
            _service = new BookService();
        }

        [Fact]
        public void GetBookQuantity_ValidBookDTO_ReturnsCorrectQuantity()
        {
            // Arrange
            var validDTO = ValidBookDTO;

            // Act
            var result = _service.GetBookQuantity(validDTO);

            // Assert
            Assert.Equal(result, ValidBookDTO.Quantity);
        }        

        [Fact]
        public void FilterByIds_GivenValidId_ReturnsCorrectBook()
        {
            // Arrange
            var validBooks = ValidBookDTOs;
            int validId = 3;

            // Act
            var filteredBooks = _service.FilterByIds(validBooks, new[] { 3 });

            // Assert
            Assert.True(filteredBooks.Single().Id == validId);
        }       
        

        [Fact]
        public void CreateDTO_GenerateDTOWithEntityData_Succes()
        {
            // Arrange
            var validBook = ValidBook;

            // Act
            var dto = _service.CreateDTO(validBook);

            // Assert
            Assert.True(dto.Id == validBook.Id);
            Assert.True(dto.Title == validBook.Title);
            Assert.True(dto.ISBN == validBook.ISBN);
            Assert.True(dto.Quantity == validBook.Quantity);
        }

        [Fact]
        public void CreateDTOS_GenerateDTOSWithEntityData_Succes()
        {
            // Arrange
            var validBooks = ValidBooks;

            // Act
            var dtos = _service.CreateDTOs(validBooks);

            // Assert

            foreach (var dto in dtos)
            {
                var validBook =
                    validBooks.First(u => u.Id == dto.Id);

                Assert.True(dto.Id == validBook.Id);
                Assert.True(dto.Title == validBook.Title);
                Assert.True(dto.ISBN == validBook.ISBN);
                Assert.True(dto.Quantity == validBook.Quantity);
            }
        }

        [Fact]
        public void CreateEntity_GenerateEntityWithDTOData_Succes()
        {
            // Arrange
            var validBookDto = ValidBookDTO;

            // Act
            var entity = _service.CreateEntity(validBookDto);

            // Assert
            Assert.True(entity.Id == validBookDto.Id);
            Assert.True(entity.Title == validBookDto.Title);
            Assert.True(entity.ISBN == validBookDto.ISBN);
            Assert.True(entity.Quantity == validBookDto.Quantity);
        }
    }
}