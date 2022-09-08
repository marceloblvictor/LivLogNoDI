using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Enums;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;
using LivlogNoDI.Validators;
using static LivlogNoDI.Enums.CustomerCategory;

namespace LivlogNoDI.Services
{
    public class CustomerBookService
    {
        private readonly CustomerBookRepository _repo;
        private readonly CustomerService _customerService;
        private readonly BookService _bookService;
        private readonly BookRentalValidator _rentalValidator;
        
        public CustomerBookService()
        {
            _repo = new CustomerBookRepository();
            _customerService = new CustomerService();
            _bookService = new BookService();
            _rentalValidator = new BookRentalValidator();
        }

        public CustomerBookDTO Get(int id)
        {
            var customerBook = _repo.Get(id);

            return CreateDTO(customerBook);
        }

        public IList<CustomerBookDTO> GetAll()
        {
            var customerBooks = _repo.GetAll();

            return CreateDTOs(customerBooks);
        }

        public IList<CustomerBookDTO> RentBooks(RentalRequestDTO request)
        {
            // Obter cliente 
            var customer = _customerService.Get(request.CustomerId);

            // Obter os livros requisitados
            var requestedBooks = _bookService.GetAll()
                .Where(b => request.BookIds.Contains(b.Id))
                .ToList();

            // Obter todos os livros de todos os clientes
            var allCustomerBooks = GetAll();

            // Filtrar os livros de clientes pertencentes ao cliente que fez o request
            var customerBooks = FilterByCustomer(
                allCustomerBooks, customer.Id);

            // Filtrar os livros do cliente que tenham o status "Ativo"
            var activeCustomerBooks = FilterByStatus(
                customerBooks, BookRentalStatus.Active);

            // Validar se o cliente pode alugar mais livros
            _rentalValidator.ValidateCustomerRentalsLimit(
                activeCustomerBooks,
                requestedBooks,
                GetCustomerRentalsMaxLimit(customer.Category));

            // Validar se existem livros suficientes no estoque
            _rentalValidator.ValidateBookAvailability(
                requestedBooks,
                allCustomerBooks);

            DateTime currentDateTime = DateTime.UtcNow;

            // Calcula o prazo do empréstimo do cliente baseado em sua categoria
            var customerDueDate = CalculateDueDate(customer, currentDateTime);

            // Cria uma nova entidade CustomerBook para cada livro alugado
            foreach (int bookId in request.BookIds)
            {
                _repo.Add(
                    CreateEntity(request.CustomerId, bookId, customerDueDate));
            }

            // Retorna uma lista com informações dos livros alugados pelo cliente
            var rentedBookDtos = CreateDTOs(_repo.GetAll());
            rentedBookDtos = FilterByCustomerAndBook(
                rentedBookDtos,
                request.CustomerId,
                request.BookIds);

            return rentedBookDtos;
        }

        #region Helper Methods

        public DateTime CalculateDueDate(CustomerDTO customer, DateTime startTime)
        {
            return startTime.AddDays(
                GetCustomerCategoryDaysDuration(customer.Category));
        }

        public int GetCustomerCategoryDaysDuration(CustomerCategory category)
            => category switch
            {
                Top => 15,
                Medium => 10,
                Low => 5,
                _ => throw new ArgumentException()
            };

        public int GetCustomerRentalsMaxLimit(CustomerCategory category)
            => category switch
            {
                Top => 5,
                Medium => 3,
                Low => 1,
                _ => throw new ArgumentException()
            };

        public IList<CustomerBookDTO> FilterByCustomer(
            IList<CustomerBookDTO> dtos,
            int customerId)
        {
            return dtos
                .Where(dto => dto.CustomerId == customerId)
                .ToList();
        }

        public IList<CustomerBookDTO> FilterByCustomerAndBook(
            IList<CustomerBookDTO> dtos,
            int customerId,
            IList<int> bookIds)
        {
            return dtos
                .Where(dto => dto.CustomerId == customerId &&
                              bookIds.Contains(dto.BookId))
                .ToList();
        }

        public IList<CustomerBookDTO> FilterByStatus(
            IList<CustomerBookDTO> dtos,
            BookRentalStatus status)
        {
            return dtos
                .Where(dto => dto.Status == status)
                .ToList();
        }

        public CustomerBookDTO CreateDTO(CustomerBook customerBook)
        {
            return new CustomerBookDTO
            {
                Id = customerBook.Id,
                BookId = customerBook.BookId,
                CustomerId = customerBook.CustomerId,
                StartDate = customerBook.DueDate
            };
        }

        public IList<CustomerBookDTO> CreateDTOs(IList<CustomerBook> customerBooks)
        {
            var customerBooksDtos = new List<CustomerBookDTO>();

            foreach (var customerBook in customerBooks)
            {
                customerBooksDtos.Add(CreateDTO(customerBook));
            }

            return customerBooksDtos;
        }

        public CustomerBook CreateEntity(int customerId, int bookId, DateTime dueDate)
        {
            return new CustomerBook
            {
                BookId = bookId,
                CustomerId = customerId,
                StartDate = DateTime.Now,
                DueDate = dueDate,
                Status = BookRentalStatus.Active
            };
        }

        #endregion
    }
}
